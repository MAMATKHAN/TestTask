using FluentMigrator;

namespace TestTask.DAL.Migrations
{
	[Migration(001, TransactionBehavior.None)]
	public class InitSchema : Migration
	{
		public override void Up()
		{
			Create.Table("contractors")
				.WithColumn("id").AsInt64().PrimaryKey("contractors_pk").Identity()
				.WithColumn("name").AsString().Unique().NotNullable()
				.WithColumn("created_at").AsDateTimeOffset().NotNullable()
				.WithColumn("updated_at").AsDateTimeOffset().NotNullable();

			Create.Table("contacts")
				.WithColumn("id").AsInt64().PrimaryKey("contacts_pk").Identity()
				.WithColumn("contractor_id").AsInt64().ForeignKey("contact_contractor_fk", "contractors", "id").NotNullable()
				.WithColumn("full_name").AsString().NotNullable()
				.WithColumn("email").AsString().Unique().NotNullable()
				.WithColumn("created_at").AsDateTimeOffset().NotNullable()
				.WithColumn("updated_at").AsDateTimeOffset().NotNullable();
		}

		public override void Down()
		{
			Delete.Table("contacts");
			Delete.Table("contractors");
		}
	}
}
