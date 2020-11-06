namespace Project.ApiRest
{
    using System.Reflection;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using Proyect.Implementation;
    using Proyect.Interface;
    using static Project.Inyection.Injection;

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
            services.AddCors();
            /*options =>
            {
                options.AddPolicy(name: MyAllowsOrigins, builder =>
                {
                    builder.WithOrigins("http://localhost:3000", "http://localhost:3000/login").
                        AllowAnyOrigin().
                        AllowAnyMethod().
                        AllowAnyHeader();
                });
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });*/



            services.AddSwaggerGen(c =>
            {                
                c.SwaggerDoc(name: "v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = $"{typeof(Startup).Assembly.GetCustomAttribute<AssemblyProductAttribute>().Product}",
                    Version = "V1",
                    Description = $"none"
                });
                /*
                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basic"
                            }
                        },
                        new string[] {}
                    }
                });*/
            });
            services.AddControllers();
            services.AddRouting();

            //configura la authenticacion basica
            //services.AddAuthentication("BasicAuthentication").
                   // AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            //services.AddScoped<IUserRepository, UserRepository>();

            

            ConfigurationServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "My API V1");
            });


            //politicas de acceso, despues de UseRouting, antes de UseAuthorization
            app.UseCors(
                x => x
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );
            
           // app.UseAuthentication();
            //app.UseAuthorization();  
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();//.RequireCors(MyAllowsOrigins);
            });       

        }
    }
}
