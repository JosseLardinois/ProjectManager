using ProjectManager.DAL;
using ProjectManager.Interfaces;
using ProjectManager.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IPhaseRepository, PhaseRepository>();
builder.Services.AddScoped<IPhaseService, PhaseService>();
builder.Services.AddScoped<IArtefactRepository, ArtefactRepository>();
builder.Services.AddScoped<IArtefactService, ArtefactService>();
builder.Services.AddScoped<IProjectOwnerRepository, ProjectOwnerRepository>();
builder.Services.AddScoped<IProjectOwnerService, ProjectOwnerService>();

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
