﻿using Dapper;
using DataAccess.Models;
using Microsoft.Data.SqlClient;
using System.Data;

const string connectionString = "Server=localhost,1433;Database=cassiano;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True;";

using (var connection = new SqlConnection(connectionString))
{
    //CreateManyCategory(connection);
    //UpdateCategory(connection);
    //ListCategories(connection);
    //CreateCategory(connection);
    //ExecuteProcedure(connection);
    //ExecuteReadProcedure(connection);
    //ExecuteScalar(connection);
    //ReadView(connection);
    //OneToOne(connection);
    //OneToMany(connection);
    //QueryMultiple(connection);
    //SelectIn(connection);
    Like(connection, "backend");
    Transaction(connection);

}

static void ListCategories(SqlConnection connection)
{
    var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
    foreach (var item in categories)
    {
        Console.WriteLine($"Id: {item.Id}, Title: {item.Title}");
    }
}

static void CreateCategory(SqlConnection connection)
{
    var category = new Category();
    category.Id = Guid.NewGuid();
    category.Title = "Amazon AWS";
    category.Url = "amazon";
    category.Summary = "AWS Cloud";
    category.Order = 8;
    category.Description = "Categoria destinada a serviços do AWS";
    category.Featured = false;

    var insertSql = @"INSERT INTO 
            [Category] 
        VALUES(
            @Id, 
            @Title, 
            @Url, 
            @Summary, 
            @Order, 
            @Description, 
            @Featured)";

    var rows = connection.Execute(insertSql, new
    {
        category.Id,
        category.Title,
        category.Url,
        category.Summary,
        category.Order,
        category.Description,
        category.Featured
    });
    Console.WriteLine($"{rows} - linhas inseridas");
}

static void UpdateCategory(SqlConnection connection)
{
    var updateQuery = @"UPDATE [Category] 
            SET [Title] = @Title
            WHERE [Id] = @Id";

    var rows = connection.Execute(updateQuery, new
    {
        id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"),
        title = "Frontend"
    });
    Console.WriteLine($"{rows} - registros atualizados");
}

static void CreateManyCategory(SqlConnection connection)
{
    var category = new Category();
    category.Id = Guid.NewGuid();
    category.Title = "Amazon AWS";
    category.Url = "amazon";
    category.Summary = "AWS Cloud";
    category.Order = 8;
    category.Description = "Categoria destinada a serviços do AWS";
    category.Featured = false;

    var category2 = new Category();
    category2.Id = Guid.NewGuid();
    category2.Title = "Categoria Nova";
    category2.Url = "Categoria-nova";
    category2.Summary = "Categoria Nova";
    category2.Order = 9;
    category2.Description = "Categoria";
    category2.Featured = true;

    var insertSql = @"INSERT INTO 
            [Category] 
        VALUES(
            @Id, 
            @Title, 
            @Url, 
            @Summary, 
            @Order, 
            @Description, 
            @Featured)";

    var rows = connection.Execute(insertSql, new[]
    {
        new
        {
        category.Id,
        category.Title,
        category.Url,
        category.Summary,
        category.Order,
        category.Description,
        category.Featured
        },
        new
        {
        category2.Id,
        category2.Title,
        category2.Url,
        category2.Summary,
        category2.Order,
        category2.Description,
        category2.Featured
        }
    });

    Console.WriteLine($"{rows} - linhas inseridas");
}

static void ExecuteProcedure(SqlConnection connection)
{
    var procedure = "[spDeleteStudent]";
    var pars = new { StudentId = "fecaaa6e-7861-4c02-8b21-b3605b7d2adb" };
    var affectedRows = connection.Execute(procedure, pars, commandType: CommandType.StoredProcedure);

    Console.WriteLine($"{affectedRows} - linhas alteradas");
}

static void ExecuteReadProcedure(SqlConnection connection)
{
    var procedure = "[spGetCoursesByCategory]";
    var pars = new { CategoryId = "09ce0b7b-cfca-497b-92c0-3290ad9d5142" };
    var courses = connection.Query(
        procedure,
        pars,
        commandType: CommandType.StoredProcedure);

    foreach (var item in courses)
    {
        Console.WriteLine(item.Title);
    }
}

static void ExecuteScalar(SqlConnection connection)
{
    var category = new Category();
    category.Title = "Amazon AWS";
    category.Url = "amazon";
    category.Summary = "AWS Cloud";
    category.Order = 8;
    category.Description = "Categoria destinada a serviços do AWS";
    category.Featured = false;

    var insertSql = @"
        INSERT INTO 
            [Category] 
        OUTPUT inserted.[Id]
        VALUES(
            NEWID(), 
            @Title, 
            @Url, 
            @Summary, 
            @Order, 
            @Description, 
            @Featured) 
        SELECT SCOPE_IDENTITY()";

    var id = connection.ExecuteScalar<Guid>(insertSql, new
    {
        category.Title,
        category.Url,
        category.Summary,
        category.Order,
        category.Description,
        category.Featured
    });
    Console.WriteLine($"A categoria inserida foi: {id}");
}

static void ReadView(SqlConnection connection)
{
    var sql = "SELECT * FROM [vwCourses]";
    var courses = connection.Query<Category>(sql);

    foreach (var item in courses)
    {
        Console.WriteLine($"Id: {item.Id}, Title: {item.Title}");
    }
}

static void OneToOne(SqlConnection connection)
{
    var sql = @"
        SELECT 
            * 
        FROM 
            [CareerItem] 
        INNER JOIN 
            [Course] ON [CareerItem].[CourseId] = [Course].[Id]";

    var items = connection.Query<CareerItem, Course, CareerItem>(
        sql,
        (CareerItem, Course) =>
        {
            CareerItem.Course = Course;
            return CareerItem;
        }, splitOn: "Id");

    foreach (var item in items)
    {
        Console.WriteLine($"{item.Title} - Curso: {item.Course}");
    }
}

static void OneToMany(SqlConnection connection)
{
    var sql = @"
        SELECT
            [Career].[Id],
            [Career].[Title],
            [CareerItem].[CareerId],
            [CareerItem].[Title]
        FROM
            [Career]
        INNER JOIN
            [CareerItem] ON [CareerItem].[CareerId] = [Career].[Id]
        ORDER BY 
        [Career].[Title]";

    var careers = new List<Career>();
    var items = connection.Query<Career, CareerItem, Career>(
        sql,
        (career, item) =>
        {
            var car = careers.Where(x => x.Id == career.Id).FirstOrDefault();
            if (car is null)
            {
                car = career;
                car.Items.Add(item);
                careers.Add(car);
            }
            else
            {
                car.Items.Add(item);
            }
            return career;
        }, splitOn: "CareerId");

    foreach (var career in careers)
    {
        Console.WriteLine($"{career.Title}");
        foreach (var item in career.Items)
        {
            Console.WriteLine($" - {item.Title}");
        }
    }
}

static void QueryMultiple(SqlConnection connection)
{
    var query = @"
        SELECT * FROM [Category];
        SELECT * FROM [Course]";

    using (var multi = connection.QueryMultiple(query))
    {
        var categories = multi.Read<Category>();
        var courses = multi.Read<Course>();

        foreach (var item in categories)
        {
            Console.WriteLine(item.Title);
        }

        foreach (var item in courses)
        {
            Console.WriteLine(item.Title);
        }
    }
}

static void SelectIn(SqlConnection connection)
{
    var query = @"
        SELECT *
        FROM
            Career
        WHERE [Id] IN @Id";

    var items = connection.Query<Career>(query, new
    {
        Id = new[]
        {
             "01ae8a85-b4e8-4194-a0f1-1c6190af54cb",
             "e6730d1c-6870-4df3-ae68-438624e04c72"
        }
    });

    foreach (var item in items)
    {
        Console.WriteLine(item.Title);
    }
}

static void Like(SqlConnection connection, string term)
{
    var query = @"
        SELECT *
        FROM
            [Course]
        WHERE [Title] LIKE @exp";

    var items = connection.Query<Career>(query, new
    {
        exp = $"%{term}%"
    });

    foreach (var item in items)
    {
        Console.WriteLine(item.Title);
    }
}

static void Transaction(SqlConnection connection)
{
    var category = new Category();
    category.Id = Guid.NewGuid();
    category.Title = "Minha categoria que não";
    category.Url = "amazon";
    category.Summary = "AWS Cloud";
    category.Order = 8;
    category.Description = "Categoria destinada a serviços do AWS";
    category.Featured = false;

    var insertSql = @"INSERT INTO 
            [Category] 
        VALUES(
            @Id, 
            @Title, 
            @Url, 
            @Summary, 
            @Order, 
            @Description, 
            @Featured)";

    using (var transaction = connection.BeginTransaction())
    {
        var rows = connection.Execute(insertSql, new
        {
            category.Id,
            category.Title,
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured
        }, transaction);

        transaction.Rollback();
        Console.WriteLine($"{rows} - linhas inseridas");
    }
}