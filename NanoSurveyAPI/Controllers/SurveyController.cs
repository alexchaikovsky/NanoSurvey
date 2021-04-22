using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NanoSurveyAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace NanoSurveyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly INanoRepository repository;

        public SurveyController(INanoRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet("{surveyId}/{questionId}")]
        public async Task<IActionResult> GetQuestion(int surveyId, int questionId)
        {
            var surveyQuestion = await repository.GetQuestion(surveyId, questionId);

            if (surveyQuestion is null)
            {
                return NotFound();

            }

            var cookieIdentifier = CookieManager.GetCookie(HttpContext) ?? await repository.AddNewInterview(surveyId);
            CookieManager.AddNewCookie(HttpContext, cookieIdentifier);

            return Ok(
                new
                {
                    Question = surveyQuestion.Question.QuestionText,
                    Answers = surveyQuestion.Question.Answers.Select(x => new { x.Id, x.AnswerText })
                });

        }

        [HttpPost("{surveyId}/{questionId}")]
        async public Task<IActionResult> PostAnswer(int surveyId, int questionId, [FromBody] List<int> answers) 
        {
            var interviewId = await repository.GetInterviewId(CookieManager.GetCookie(HttpContext));
            if (interviewId is null)
            {
                return Unauthorized();
            }

            await repository.SaveResult(questionId, answers, interviewId.Value);

            var nextQuestionId = await repository.GetNextQuestionId(surveyId, questionId);
            if (nextQuestionId is null)
            {
                CookieManager.ExpireCookie(HttpContext);
                return Ok();
            }
            return Ok(new { id = nextQuestionId.Value });
        }
    }
}
