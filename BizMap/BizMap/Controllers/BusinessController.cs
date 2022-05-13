using BizMap.DataTransfer;
using BizMap.Models;
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
                    Name = n.BusinessName,
                    Distance = "0Km",
                    Latitude = n.Latitute,
                    Longitude = n.Longitute
                }).ToList()
            };

            return Ok(result);

        }

        [HttpPost("business/{id}")]
        public async Task<ActionResult<StatusResponse>> CreateBusiness(string id, [FromBody] BusinessModel model)
        {
            if (string.IsNullOrWhiteSpace(id))
                return StatusCode(428, new StatusResponse { Message = "A valid reference has not been provided", Status = false });

            if (!ModelState.IsValid)
                return StatusCode(428, new StatusResponse { Message = "Required entries missing", Status = false });


            var findStore = await _context.BizStores.FirstOrDefaultAsync(h => h.Id == id);
            var findUser = await _context.BizUsers.AnyAsync(b => b.Id == id);
            if (!findUser && findStore is null)
            {
                await _context.BizUsers.AddAsync(new BusinessUser { Id = id, FullName = model.FullName, Gender = model.Gender });
                findUser = true;
            }


            if (findStore is not null)
            {
                findStore.BusinessName = model.Name;
                findStore.Latitute = (float)model.Latitude;
                findStore.Longitute = (float)model.Longitude;
                findStore.Address = model.Address;
                findStore.Country = model.Country;
                findStore.Category = model.Category;
                findStore.City = model.City;
                findStore.State = model.State;

                _context.BizStores.Update(findStore);
                await _context.SaveChangesAsync();
                return Ok(new StatusResponse { Message = $"your business store was updated successfully", Status = true });
            }
           
            if(!findUser)
                return StatusCode(428, new StatusResponse { Message = "A valid user reference is required to create a new business store", Status = false });


            var rng = new Random();
            var newBiz = new BusinessStore { Latitute = (float)model.Latitude, Longitute = (float)model.Longitude, City = model.City, Country = model.Country, Address = model.Address, BizCode = rng.Next(100000, 999999).ToString(), BusinessName = model.Name, BusinessUserId = id, Category = model.Category, State = model.State };
            await _context.BizStores.AddAsync(newBiz);

            await _context.SaveChangesAsync();
            return Ok(new StatusResponse { Message = $"your business store was creatd successfully", Status = true });
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
                Information = new BusinessInformation
                {
                    Address = findMyBusiness.Address,
                    BusinessCode = findMyBusiness.BizCode,
                    Category = findMyBusiness.Category,
                    Id = findMyBusiness.Id,
                    Name = findMyBusiness.BusinessName,
                    City = findMyBusiness.City,
                    Country = findMyBusiness.Country,
                    State = findMyBusiness.State,
                    Latitude = findMyBusiness.Latitute,
                    Longitude = findMyBusiness.Longitute,
                }
            };

            return Ok(result);

        }

        [HttpGet("findbusiness")]
        public async Task<ActionResult<UserBusinesses>> FindBusinesses([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] string category = null, [FromQuery] char mode = 'K')
        {
            var distance = 1000.0;
            var Radius = mode == 'M' || mode == 'm' ? 3959 : 6371.0;

            var maxLat = latitude + Rad2deg(distance / Radius);
            var minLat = latitude - Rad2deg(distance / Radius);
            var maxLon = longitude + Rad2deg(Math.Asin(distance / Radius) / Math.Cos(Deg2rad(latitude)));
            var minLon = longitude - Rad2deg(Math.Asin(distance / Radius) / Math.Cos(Deg2rad(latitude)));

            var findBusiness =  string.IsNullOrWhiteSpace(category) ? await _context.BizStores.Where(l => l.Latitute >= minLat && l.Latitute <= maxLat && l.Longitute >= minLon && l.Longitute <= maxLon).ToListAsync() : await _context.BizStores.Where(l => l.Latitute >= minLat && l.Latitute <= maxLat && l.Longitute >= minLon && l.Longitute <= maxLon && EF.Functions.Like(l.Category.ToLower(), category.ToLower())).ToListAsync();

            var summary = mode == 'K'? "Km" : "Ms";
            var result = new UserBusinesses
            {
                Status = true,
                Message = "Successful",
                MyBusiness = findBusiness.Select(n => new BusinessSummary
                {
                    Address = n.Address,
                    BusinessCode = n.BizCode,
                    Category = n.Category,
                    Id = n.Id,
                    Name = n.BusinessName,
                    Distance = $"{(int)Distance(latitude, longitude, n.Latitute, n.Longitute, mode)}{summary}",
                    Latitude = n.Latitute,
                    Longitude = n.Longitute
                }).ToList()
            };

            return Ok(result);
        }

        private static double Rad2deg(double rad)
        {
            var result = rad / Math.PI * 180.0;
            return result;
        }

        private static double Deg2rad(double deg)
        {
            var result = deg * Math.PI / 180.0;
            return result;
        }

        private static double Distance(double queryLat, double queryLon, double dbLat, double dblon, char unit)
        {
            if ((queryLat == dbLat) && (queryLon == dblon))
                return 0;

            double theta = queryLon - dblon;
            double dist = Math.Sin(Deg2rad(queryLat)) * Math.Sin(Deg2rad(dbLat)) + Math.Cos(Deg2rad(queryLat)) * Math.Cos(Deg2rad(dbLat)) * Math.Cos(Deg2rad(theta));
            dist = Math.Acos(dist);
            dist = Rad2deg(dist);
            dist = dist * 60 * 1.1515;

            if (unit == 'K')
                return dist *= 1.609344;
            else if(unit == 'N')
                return dist *= 0.8684;

            return dist;
        }

    }
}
