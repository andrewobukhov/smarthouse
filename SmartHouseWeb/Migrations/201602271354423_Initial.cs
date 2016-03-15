namespace SmartHouseWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Room",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RoomId);
            
            CreateTable(
                "dbo.Sensor",
                c => new
                    {
                        SensorId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RoomId = c.Int(nullable: false),
                        SensorIndex = c.Int(nullable: false),
                        IsStatisticEnable = c.Boolean(nullable: false),
                        StatisticFrequency = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SensorId)
                .ForeignKey("dbo.Room", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.SensorState",
                c => new
                    {
                        SensorStateId = c.Int(nullable: false, identity: true),
                        SensorId = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SensorStateId)
                .ForeignKey("dbo.Sensor", t => t.SensorId, cascadeDelete: true)
                .Index(t => t.SensorId);
            
            CreateTable(
                "dbo.SensorStatistic",
                c => new
                    {
                        SensorStatisticId = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        SensorId = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SensorStatisticId)
                .ForeignKey("dbo.Sensor", t => t.SensorId, cascadeDelete: true)
                .Index(t => t.SensorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SensorStatistic", "SensorId", "dbo.Sensor");
            DropForeignKey("dbo.SensorState", "SensorId", "dbo.Sensor");
            DropForeignKey("dbo.Sensor", "RoomId", "dbo.Room");
            DropIndex("dbo.SensorStatistic", new[] { "SensorId" });
            DropIndex("dbo.SensorState", new[] { "SensorId" });
            DropIndex("dbo.Sensor", new[] { "RoomId" });
            DropTable("dbo.SensorStatistic");
            DropTable("dbo.SensorState");
            DropTable("dbo.Sensor");
            DropTable("dbo.Room");
        }
    }
}
