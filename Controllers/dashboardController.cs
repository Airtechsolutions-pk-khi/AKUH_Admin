
using System;
using System.Collections.Generic;
using AKU_Admin._Models;
using AKU_Admin.BLL._Services;
using Microsoft.AspNetCore.Mvc;

namespace AKU_Admin.Controllers
{
    [Route("api/[controller]")]

    public class dashboardController : ControllerBase
    {
        dashboardService _service;
        public dashboardController()
        {
            _service = new dashboardService();
        }

        [HttpGet("all")]
        public List<DashboardSummary> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("getcharts")]
        public RspDashboard GetChart()
        {
            return _service.GetChart();
        }
        [HttpGet("getchartsMonth")]
        public RspDashboard getchartsMonth()
        {
            return _service.getchartsMonth();
        }
    }
}
