using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.HostedServices;
using Data.Context;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Stories;
using Application.Synchronizers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MediatR;
using Application.BestStories;
using AutoMapper;

namespace API
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
            services.Configure<HackerNewsOptions>(Configuration.GetSection(HackerNewsOptions.HackerNews));

            services.AddDbContext<StoryContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlite(Configuration.GetConnectionString("ItemDbConnection"));
            });

            services.AddHostedService<SynchronizerManagerHostedService>();
            services.AddScoped<ISynchronizer, Synchronizer>();
            services.AddTransient<IStoriesAccessor, StoriesAccessor>();
            services.AddTransient<IStoryRepository, StoryRepository>();

            services.AddMediatR(typeof(List).Assembly);
            services.AddAutoMapper(typeof(List).Assembly, typeof(StoriesAccessor).Assembly);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMvc();
        }
    }
}
