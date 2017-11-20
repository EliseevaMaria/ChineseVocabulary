namespace Vocabulary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIsDirty : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Words", "IsDirty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Words", "IsDirty", c => c.Boolean(nullable: false));
        }
    }
}
