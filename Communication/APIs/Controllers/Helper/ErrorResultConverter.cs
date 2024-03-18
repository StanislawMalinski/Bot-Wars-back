using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;

namespace Communication.APIs.Controllers.Helper;

public static class ErrorResultConverter
{
    public static IActionResult ErrorResult(this ControllerBase controller, IErrorResult errorResult) =>
        errorResult switch
        {
            AccessDeniedError => controller.BadRequest(),
            EntityNotFoundErrorResult => controller.NotFound(errorResult),
            NotImplementedError => controller.BadRequest(errorResult),
            AlreadyRegisterForTournamentError => controller.BadRequest(errorResult),
            AchievementsAlreadyObtainedError => controller.BadRequest(errorResult),
            NotEnoughAchievementPointsError => controller.BadRequest(errorResult),
            BadAccountInformationError => controller.NotFound(errorResult),
            TournamentIsBeingPlayedError => controller.BadRequest(errorResult),
            IncorrectOperation => controller.BadRequest(errorResult),
            _ => controller.StatusCode((int)HttpStatusCode.InternalServerError, errorResult)
        };
}