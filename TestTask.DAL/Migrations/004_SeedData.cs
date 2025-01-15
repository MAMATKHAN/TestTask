using FluentMigrator;

namespace TestTask.DAL.Migrations
{
	[Migration(004, TransactionBehavior.None)]
	public class SeedData : Migration
	{
		public override void Up()
		{
			const string sql = @"
INSERT INTO contractors (name, created_at, updated_at)
VALUES ('Contractor A', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
	 , ('Contractor B', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
     , ('Contractor C', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
     , ('Contractor D', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
     , ('Contractor E', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO contacts (contractor_id, full_name, email, created_at, updated_at)
VALUES (1, 'John Doe', 'john.doe@example.com', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
     , (1, 'Jane Smith', 'jane.smith@example.com', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
     , (1, 'Alice Johnson', 'alice.johnson@example.com', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
     , (2, 'Bob Brown', 'bob.brown@example.com', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
     , (2, 'Charlie Davis', 'charlie.davis@example.com', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
     , (2, 'Diana Evans', 'diana.evans@example.com', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
     , (3, 'Frank Harris', 'frank.harris@example.com', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
     , (3, 'Grace Kelly', 'grace.kelly@example.com', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
     , (3, 'Henry Lewis', 'henry.lewis@example.com', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
     , (4, 'Isabel Moore', 'isabel.moore@example.com', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
     , (4, 'Jack Nelson', 'jack.nelson@example.com', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
     , (4, 'Karen OBrien', 'karen.obrien@example.com', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
     , (5, 'Larry Perez', 'larry.perez@example.com', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
     , (5, 'Monica Quinn', 'monica.quinn@example.com', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
     , (5, 'Nathan Roberts', 'nathan.roberts@example.com', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
";

            Execute.Sql(sql);
        }

		public override void Down()
		{
            Delete.FromTable("contacts").AllRows();
            Delete.FromTable("contractors").AllRows();
		}
	}
}
