﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchMvc.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Product(Name, Description,Price,Stock,Image,CategoryId)" +
                                 "VALUES('Caderno espiral','Caderdo espiral 100 folhas',7.45,50,'caderno1.jpg',1)");
            
            migrationBuilder.Sql("INSERT INTO Product(Name, Description,Price,Stock,Image,CategoryId)" +
                                 "VALUES('Estojo escolar','Estojo escolar cinza',5.65,70,'estojo1.jpg',1)");
            
            migrationBuilder.Sql("INSERT INTO Product(Name, Description,Price,Stock,Image,CategoryId)" +
                                 "VALUES('Borracha escolar','Borracha branca pequena',3.25,80,'borracha1.jpg',1)");
            
            migrationBuilder.Sql("INSERT INTO Product(Name, Description,Price,Stock,Image,CategoryId)" +
                                 "VALUES('Calculadora escolar','Calculadora simples',15.39,20,'calculadora1.jpg',2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Product");
        }
    }
}
