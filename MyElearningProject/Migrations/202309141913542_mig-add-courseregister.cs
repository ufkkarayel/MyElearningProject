namespace MyElearningProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migaddcourseregister : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "Category_CategoryID", "dbo.Categories");
            DropIndex("dbo.Courses", new[] { "Category_CategoryID" });
            RenameColumn(table: "dbo.Courses", name: "Category_CategoryID", newName: "CategoryID");
            CreateTable(
                "dbo.CourseRegisters",
                c => new
                    {
                        CourseRegisterID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CourseRegisterID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID)
                .Index(t => t.CourseID);
            
            AlterColumn("dbo.Courses", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "CategoryID");
            AddForeignKey("dbo.Courses", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.CourseRegisters", "StudentID", "dbo.Students");
            DropForeignKey("dbo.CourseRegisters", "CourseID", "dbo.Courses");
            DropIndex("dbo.CourseRegisters", new[] { "CourseID" });
            DropIndex("dbo.CourseRegisters", new[] { "StudentID" });
            DropIndex("dbo.Courses", new[] { "CategoryID" });
            AlterColumn("dbo.Courses", "CategoryID", c => c.Int());
            DropTable("dbo.CourseRegisters");
            RenameColumn(table: "dbo.Courses", name: "CategoryID", newName: "Category_CategoryID");
            CreateIndex("dbo.Courses", "Category_CategoryID");
            AddForeignKey("dbo.Courses", "Category_CategoryID", "dbo.Categories", "CategoryID");
        }
    }
}
