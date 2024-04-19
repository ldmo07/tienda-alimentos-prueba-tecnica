using Datos.Implementacion;
using Datos.Interfaces;
using Helpers.Implementacion;
using Helpers.Intrefaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Modelos.Dtos;
using Modelos.Response;
using Negocios.Categoria;
using System.Text;
using WEBAPI.Modelos;


var builder = WebApplication.CreateBuilder(args);

//leo la configuracion del smtp
builder.Services.Configure<ConfiguracionSmtp>(builder.Configuration.GetSection("ConfiguracionSmtp"));

#region JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CONFIGURO LOS CORS
builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyHeader())
);

//configuro MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(InsertarCategoriaHandler).Assembly));

//inyeccion de dependencias
builder.Services.AddScoped(typeof(IData<ResponseModel, CategoriaDto>), typeof(DataCategoria));
builder.Services.AddScoped(typeof(IData<ResponseModel, ProductoDto>), typeof(DataProducto));
//builder.Services.AddScoped(typeof(IData<ResponseModel, PedidoDto>), typeof(DataPedido));
builder.Services.AddScoped(typeof(IData<ResponseModel, UsuarioDto>), typeof(DataUsuario));
builder.Services.AddScoped<IDataInformacionPedidoDetalle, DataInformacionPedidoDetalle>();
builder.Services.AddScoped<IJwtGenerador, JwtGenerador>();
builder.Services.AddScoped<IDataUsuario, DataUsuario>();
builder.Services.AddScoped<IBecryptHelper, BecryptHelper>();
builder.Services.AddScoped<IEmailHelper, EmailHelper>();
builder.Services.AddScoped<IDataListInformacionCompra, DataListInformacionCompra>();

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()||app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
