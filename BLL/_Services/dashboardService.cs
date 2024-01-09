using BAL.Repositories;
using DAL.Models;
using AKU_Admin._Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin.BLL._Services
{
    public class dashboardService : baseService
    {
        dashboardDB _service;
        public dashboardService()
        {
            _service = new dashboardDB();
        }

        public List<DashboardSummary> GetAll()
        {
            try
            {
                return _service.GetDashboardSummary();
            }
            catch (Exception ex)
            {
                return new List<DashboardSummary>();
            }
        }
        public RspDashboard GetChart()
        {
            var rsp = new RspDashboard();
            try
            {
                rsp.maensales = _service.GetMAENSummary();
            }
            catch (Exception)
            {
                rsp.maensales = new DashboardMAEN();
            }
            try
            {
                rsp.todaysales = _service.GetTodaySales();
            }
            catch (Exception)
            {
                rsp.todaysales = new DashboardToday();
            }

            return rsp;

        }
    }
}
