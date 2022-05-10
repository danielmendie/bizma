using BizMap.DataTransfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Skybrinns.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizMap.Controllers
{
    [Route("api")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly BizDbContext _context;
        private readonly ILogger<BusinessController> _logger;

        public BusinessController(BizDbContext context, ILogger<BusinessController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet("business/{id}")]
        public async Task<ActionResult<UserBusinesses>> GetUserBusiness(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return StatusCode(428, new UserBusinesses { Message = "Please specify a valid user reference id", Status = false });

            var findMyBusiness = await _context.BizStores.Where(j => j.BusinessUserId == id).ToListAsync();
            var result = new UserBusinesses
            {
                Status = true,
                Message = "Successful",
                MyBusiness = findMyBusiness.Select(n => new BusinessSummary
                {
                    Address = n.Address,
                    BusinessCode = n.BizCode,
                    Category = n.Category,
                    Id = n.Id,
                    Name = n.BusinessName
                }).ToList()
            };

            return Ok(result);

        }

        [HttpPost("business/{id}")]
        public async Task<ActionResult<UserBusinesses>> CreateBusiness(string id)
        {

            return StatusCode(201);
        }


        [HttpGet("business/{id}/info")]
        public async Task<ActionResult<BusinessInfo>> GetBusinessInforamtion(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return StatusCode(428, new BusinessInfo { Message = "Please specify a valid business id", Status = false });

            var findMyBusiness = await _context.BizStores.FirstOrDefaultAsync(j => j.Id == id);
            var result = new BusinessInfo
            {
                Status = true,
                Message = "Successful",
                Information =  new BusinessInformation
                {
                    Address = findMyBusiness.Address,
                    BusinessCode = findMyBusiness.BizCode,
                    Category = findMyBusiness.Category,
                    Id = findMyBusiness.Id,
                    Name = findMyBusiness.BusinessName,
                     City= findMyBusiness.City,
                     Country= findMyBusiness.Country,
                      State= findMyBusiness.State,
                     Latitude= findMyBusiness.Latitute,
                     Longitude= findMyBusiness.Longitute,
                }
            };

            return Ok(result);

        }


    }
}
