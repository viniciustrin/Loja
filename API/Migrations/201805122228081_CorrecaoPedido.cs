namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrecaoPedido : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Pedidoes", name: "CarrinhoItensId", newName: "CarrinhoId");
            RenameIndex(table: "dbo.Pedidoes", name: "IX_CarrinhoItensId", newName: "IX_CarrinhoId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Pedidoes", name: "IX_CarrinhoId", newName: "IX_CarrinhoItensId");
            RenameColumn(table: "dbo.Pedidoes", name: "CarrinhoId", newName: "CarrinhoItensId");
        }
    }
}
