using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
