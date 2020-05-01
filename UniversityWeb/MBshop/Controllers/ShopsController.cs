using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MBshop.Models;
using Microsoft.AspNetCore.Authorization;
using MBshop.Data.Data;
using System.Security.Claims;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using MBshop.Service.Services;
using MBshop.Service.interfaces;

namespace MBshop.Controllers
{

    public class ShopsController : Controller
    {
        private readonly IAdminPanel adminPanel;
        private string result = "";

        public ShopsController(
            IAdminPanel adminPanel)
        {
            this.adminPanel = adminPanel;
        }

        // GET: Shops
        [Authorize(Roles = "Admin,Moderator")]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public IActionResult Index()
        {
            var shops = this.adminPanel.ViewShops();

            return View(shops);
        }

        // GET: Shops/Delete/5
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shops = await this.adminPanel.ChekViewShop((int)id);

            if (shops == null)
            {
                return NotFound();
            }

            return this.View(shops);
        }

        // POST: Shops/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                this.result = await this.adminPanel.DeleteViewShops(id);
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException("Somthing goes wrong with Delete shops in db", e);
            }

            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin,Moderator")]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public IActionResult Logs() => this.View(this.adminPanel.LoggedUsers());


        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("DeleteLog")]
        [ValidateAntiForgeryToken]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteLog(string name, string userName, string hooks, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLog = this.adminPanel.ChekForLog(userName, (int)id);

            if (name == "%name-no-name%" && userLog.UserLoged == userName && hooks == "%sid-ni-as-no-one%" && userLog.LogId == id)
            {
                //To Do globalAlertMessages
                try
                {
                    this.result = await this.adminPanel.DeleteLogsAfterTheChek(userName, (int)id);
                }
                catch (InvalidOperationException e)
                {

                    throw new InvalidOperationException("Somthing goes wrong with deleting single logs in db", e);
                }
            }

            return this.RedirectToAction("Logs", "Shops");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("DeleteLogs")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLogs(string name, string userName, string hooks, int? id)
        {
            try
            {
                this.result = await this.adminPanel.DeleteAllLogs();
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException("Somthing goes wrong with Deleting all logs from db", e);
            }

            return this.RedirectToAction("Logs", "Shops");
        }
    }
}