namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProdutoCategoriaModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProdutoCategorias",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProdutoId, t.CategoriaId })
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .ForeignKey("dbo.Produtoes", t => t.ProdutoId, cascadeDelete: true)
                .Index(t => t.ProdutoId)
                .Index(t => t.CategoriaId);
            
            AlterColumn("dbo.Produtoes", "Nome", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Produtoes", "Desc", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProdutoCategorias", "ProdutoId", "dbo.Produtoes");
            DropForeignKey("dbo.ProdutoCategorias", "CategoriaId", "dbo.Categorias");
            DropIndex("dbo.ProdutoCategorias", new[] { "CategoriaId" });
            DropIndex("dbo.ProdutoCategorias", new[] { "ProdutoId" });
            AlterColumn("dbo.Produtoes", "Desc", c => c.String());
            AlterColumn("dbo.Produtoes", "Nome", c => c.String());
            DropTable("dbo.ProdutoCategorias");
        }
    }
}
