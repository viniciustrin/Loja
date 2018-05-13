namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApiModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carrinhoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.CarrinhoItens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarrinhoId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        ValorUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorTotalItem = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantidade = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carrinhoes", t => t.CarrinhoId, cascadeDelete: true)
                .ForeignKey("dbo.Produtoes", t => t.ProdutoId, cascadeDelete: true)
                .Index(t => t.CarrinhoId)
                .Index(t => t.ProdutoId);
            
            CreateTable(
                "dbo.Produtoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Desc = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecoPromo = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pedidoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CarrinhoItensId = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarrinhoItens", t => t.CarrinhoItensId, cascadeDelete: true)
                .Index(t => t.CarrinhoItensId);
            
            CreateTable(
                "dbo.PedidoItens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PedidoId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        ValorUnidade = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantidade = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pedidoes", t => t.PedidoId, cascadeDelete: true)
                .ForeignKey("dbo.Produtoes", t => t.ProdutoId)
                .Index(t => t.PedidoId)
                .Index(t => t.ProdutoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PedidoItens", "ProdutoId", "dbo.Produtoes");
            DropForeignKey("dbo.PedidoItens", "PedidoId", "dbo.Pedidoes");
            DropForeignKey("dbo.Pedidoes", "CarrinhoItensId", "dbo.CarrinhoItens");
            DropForeignKey("dbo.CarrinhoItens", "ProdutoId", "dbo.Produtoes");
            DropForeignKey("dbo.CarrinhoItens", "CarrinhoId", "dbo.Carrinhoes");
            DropForeignKey("dbo.Carrinhoes", "ClienteId", "dbo.Clientes");
            DropIndex("dbo.PedidoItens", new[] { "ProdutoId" });
            DropIndex("dbo.PedidoItens", new[] { "PedidoId" });
            DropIndex("dbo.Pedidoes", new[] { "CarrinhoItensId" });
            DropIndex("dbo.CarrinhoItens", new[] { "ProdutoId" });
            DropIndex("dbo.CarrinhoItens", new[] { "CarrinhoId" });
            DropIndex("dbo.Carrinhoes", new[] { "ClienteId" });
            DropTable("dbo.PedidoItens");
            DropTable("dbo.Pedidoes");
            DropTable("dbo.Produtoes");
            DropTable("dbo.CarrinhoItens");
            DropTable("dbo.Carrinhoes");
        }
    }
}
