#region IMPORTA��O REFERENTE AO BANCO DE DADOS
using UrbanTech.Data.Contexts;
using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using UrbanTech.Models;
using UrbanTech.ViewModel;
#endregion

public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            #region INICIALIZANDO O BANCO DE DADOS
            var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
            builder.Services.AddDbContext<DatabaseContext>(
                opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true)
            );
            #endregion

            #region AutoMapper
            var mapperConfig = new AutoMapper.MapperConfiguration(c => {
                c.AllowNullCollections = true;
                c.AllowNullDestinationValues = true;

                c.CreateMap<ChamadoModel, ChamadoCreateViewModel>();
                c.CreateMap<ChamadoCreateViewModel, ChamadoModel>();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);
            #endregion

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

    }
