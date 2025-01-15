using FluentMigrator;

namespace TestTask.DAL.Migrations
{
	[Migration(002, TransactionBehavior.None)]
	public class AddContactV1Type : Migration
	{
		public override void Up()
		{
			const string sql = @"
DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'contacts_v1') THEN
            CREATE TYPE contacts_v1 as
            (
                  id          bigint
				, contractor_id bigint
				, full_name	  text
                , email       text
                , created_at  timestamp with time zone
                , updated_at  timestamp with time zone
            );
        END IF;
    END
$$;";

			Execute.Sql(sql);
		}

		public override void Down()
		{
			const string sql = @"
DO $$
    BEGIN
        DROP TYPE IF EXISTS contacts_v1;
    END
$$;";

			Execute.Sql(sql);
		}
	}
}
