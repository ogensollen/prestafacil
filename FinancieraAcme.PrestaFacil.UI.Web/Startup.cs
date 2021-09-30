using FinancieraAcme.PrestaFacil.Domain.Interfaces;
using FinancieraAcme.PrestaFacil.Infraestructure.Data.Model;
using FinancieraAcme.PrestaFacil.Infraestructure.Data.Repositories;
using FinancieraAcme.PrestaFacil.Infraestructure.Data.UnitOfWork;
using FinancieraAcme.PrestaFacil.UI.Web.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FinancieraAcme.PrestaFacil.UI.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // se habilita los servicios para uso de sesiones (Session Managament)
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = System.TimeSpan.FromMinutes(20);
                options.Cookie.IsEssential = true;
                options.Cookie.HttpOnly = true;
            });

            // fuentes de datos
            services.AddDbContext<PrestaFacilDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("PrestaFacilDbContextConnection"), x => x.MigrationsAssembly("FinancieraAcme.PrestaFacil.Infraestructure.Data")));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // repositorios
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ISolicitudPrestamoRepository, SolicitudPrestamoRepository>();
            services.AddScoped<ISolicitudCabeceraRepository, SolicitudCabeceraRepository>();
            services.AddScoped<ISolicitudDetalleRepository, SolicitudDetalleRepository>();
            // repositorios...

            // unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // para habilitar el uso de sesiones
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
