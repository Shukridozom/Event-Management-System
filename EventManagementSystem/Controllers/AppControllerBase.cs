using AutoMapper;
using EventManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json.Nodes;

namespace EventManagementSystem.Controllers
{
    public class AppControllerBase : ControllerBase
    {
        protected readonly AppDbContext context;
        protected readonly IConfiguration config;
        protected readonly IMapper mapper;

        public AppControllerBase(AppDbContext context, IConfiguration config, IMapper mapper)
        {
            this.context = context;
            this.config = config;
            this.mapper = mapper;
        }

        protected string GenerateJsonErrorResponse(string property, string message)
        {
            var response = new JsonObject();
            var errors = new JsonObject();
            var messages = new JsonArray();
            messages.Add(message);
            errors.Add(property, messages);
            response.Add("errors", errors);

            return response.ToString();
        }

        protected int GetUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var userClaims = identity.Claims;

            return Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
