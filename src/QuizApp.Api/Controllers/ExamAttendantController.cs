﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.ExamAttendants;
using QuizApp.Application.ExamAttendants.GetExamAttendantsByExam;
using QuizApp.Domain.Shared;

namespace QuizApp.Api.Controllers;

[Route("api/examAttendant")]
public class ExamAttendantController : ApiController
{
    public ExamAttendantController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateExamAttendant(
        CreateExamAttendantCommand command,
        CancellationToken token)
    {
        Result<ExamAttendantResponse> response = await Sender.Send(command, token);

        return response.IsSuccess ? Ok(response) : BadRequest(response.Error);
    }
    [HttpGet("examId:Guid")]
    public async ValueTask<IActionResult> GetExamApplicantByExamId(
        Guid examId,
        CancellationToken token)
    {
        var query = new GetExamAttendantByExamQuery(examId);
        var response = await Sender.Send(query, token);
        return response.IsSuccess ? Ok(response) : BadRequest(response.Error);
    }
}