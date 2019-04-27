using Microsoft.EntityFrameworkCore.Migrations;

namespace hNext.DbAccessMSSQLCore.Migrations
{
    public partial class PatientSearchFunction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string function =
                @"CREATE FUNCTION SearchPatients (@name nvarchar(MAX),
                    @year int, @regionId int, @districtId int, @cityId int)
                RETURNS TABLE
                AS
                RETURN(
                    SELECT Pat.*
                    FROM Patients AS Pat
                    INNER JOIN People AS P ON Pat.PersonId = P.Id
                    INNER JOIN Addresses AS A ON P.AddressId = A.Id
                    WHERE (@name IS NULL OR P.FamilyName LIKE @name+'%')
                    AND (@year IS NULL OR YEAR(P.DateOfBirth) = @year)
                    AND (@regionId IS NULL OR A.RegionId = @regionId)
                    AND (@districtId IS NULL OR A.DistrictId = @districtId)
                    AND (@cityId IS NULL OR A.CityId = @cityId));";
            migrationBuilder.Sql(function);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string drop =
                @"DROP FUNCTION SearchPatients";
            migrationBuilder.Sql(drop);
        }
    }
}
