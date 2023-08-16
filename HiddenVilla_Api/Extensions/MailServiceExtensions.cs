using Business.Config;
using Business.Contracts;
using Business.Repository;
using DataAccess.Data;
using HiddenVilla_Api.Config;
using HiddenVilla_Api.Email;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace HiddenVilla_Api.Extensions
{
    public static class MailServiceExtensions
    {
        public static IServiceCollection AddMyMailServices(this IServiceCollection services, IConfiguration config)

        {
			var appMailSettingsSection = config.GetSection("MailJetSettings");
			services.Configure<MailJetSettings>(appMailSettingsSection);

			services.AddScoped<IEmailSender, EmailJetSender>();

            return services;
        }

    }
}
