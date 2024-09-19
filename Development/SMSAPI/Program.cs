
using Microsoft.OpenApi.Models;
using SMSAPI.Authentication;
using SMSAPI.Data;
using SMSAPI.Interfaces;
using SMSAPI.Repository;

namespace SMSAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IClassRoomRepository, ClassRoomRepository>();
            builder.Services.AddScoped<IPupilRepository, PupilRepository>();
            builder.Services.AddScoped<IReportCardRepository, ReportCardRepository>();
            builder.Services.AddScoped<IReportCardSubjectRepository, ReportCardSubjectRepository>();
            builder.Services.AddScoped<IGuardianRepository, GuardianRepository>();
            builder.Services.AddScoped<IGuardianContactRepository, GuardianContactRepository>();
            builder.Services.AddScoped<IGuardianContactRepository, GuardianContactRepository>();
            builder.Services.AddScoped<IGradeRepository, GradeRepository>();
            builder.Services.AddScoped<IRegisterRepository, RegisterRepository>();
            builder.Services.AddScoped<IClassRegisterRepository, ClassRegisterRepository>();
            builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
            builder.Services.AddScoped<IMealPaymentRepository, MealPaymentRepository>();
            builder.Services.AddScoped<IMealPreparedRepository, MealPreparedRepository>();
            builder.Services.AddScoped<ITeacherContactRepository, TeacherContactRepository>();
            builder.Services.AddScoped<ITeacherAddressRepository, TeacherAddressRepository>();
            builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IStaffRepository, StaffRepository>();

            builder.Services.AddScoped<AuthFilter>();
            builder.Services.AddScoped<IAuthentication, Authentication_>();
            builder.Services.AddEndpointsApiExplorer(); 

            //api endpoint security section
            builder.Services.AddSwaggerGen(/*x =>
            {
                x.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Description = "The Api Key to access the EndPoint",
                    Type = SecuritySchemeType.ApiKey,
                    Name = "SMS-APi-Key",
                    In = ParameterLocation.Header,
                    Scheme = "ApiKeyScheme"
                });

                var scheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "ApiKey",
                    },
                    In = ParameterLocation.Header,
                };

                var requirement = new OpenApiSecurityRequirement
                {
                    {
                        scheme,
                        new List<string>()
                    }
                };
                x.AddSecurityRequirement(requirement);
            }*/);


            //datacontext section
            builder.Services.AddDbContext<DataContext>(options =>
            {
                //sql connection logic here
               //options.UseSqlServer(builder.Configuration.GetConnectionString("SMSConnectionString"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(); 
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
