using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CoreApiVs.Data;
using CoreApiVs.Data.Entities;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MainDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("default")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/", () => "Hello to .Net 6 and C# 10");
app.MapGet("/api/user", async (MainDbContext db) => await db.User.ToListAsync());
app.MapGet("/api/user/{id}", async (int id, MainDbContext db) =>
{
    return await db.User.Where(u => u.UserId == id).SingleOrDefaultAsync();
});
app.MapPost("/api/user", async (User u1, [FromServices] MainDbContext db) =>
{
    db.User.Add(u1);
    await db.SaveChangesAsync();
    return Results.Created("/api/user/" + u1.UserId, u1);
});
app.MapPut("/api/user", async (User user, [FromServices] MainDbContext db) =>
{
    db.Update(user);
    await db.SaveChangesAsync();
    return Results.Ok(user);
});
app.MapDelete("/api/user/{id}", async (int id, MainDbContext db) =>
{
    var user = await db.User.Where(u => u.UserId == id).SingleOrDefaultAsync();
    if (user == null) return Results.NotFound();
    db.User.Remove(user);
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.Run();

