using System;
using System.Threading.Tasks;
using System.Web.Security;
using Forte.Migrations;

namespace EpiDemo.Web.Migrations
{
    [Migration("7B05394A-3417-4AF1-8AAB-07BA7285F59E")]
    public class CreateAdministratorAccounts : IMigration
    {
        private readonly string[] Emails = new[]
        {
            "admin@example.com",
        };

        private const string Password = "Admin123!";
        private const string AdminRole = "Administrators";
        private const string EditorRole = "WebEditors";
        
        public Task ExecuteAsync()
        {
            if (!Roles.RoleExists(AdminRole))
                Roles.CreateRole(AdminRole);
            if (!Roles.RoleExists(EditorRole))
                Roles.CreateRole(EditorRole);
            foreach (var emailAddress in Emails)
            {
                CreateAdministrator(emailAddress);
            }
            
            return Task.CompletedTask;
        }

        private void CreateAdministrator(string emailAddress)
        {
            Membership.CreateUser(emailAddress, Password, emailAddress);
            Roles.AddUserToRole(emailAddress, AdminRole);
        }
    }
}