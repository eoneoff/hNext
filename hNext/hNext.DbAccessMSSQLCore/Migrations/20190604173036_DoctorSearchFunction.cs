using Microsoft.EntityFrameworkCore.Migrations;

namespace hNext.DbAccessMSSQLCore.Migrations
{
    public partial class DoctorSearchFunction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string function =
                @"CREATE FUNCTION SearchDoctors (@name nvarchar(MAX),
                    @specialtyId int, @hospitalId int, @departmentId int)
                RETURNS TABLE
                AS
                RETURN(
                SELECT D.*
                FROM Doctors AS D
                INNER JOIN People as P ON P.Id = D.PersonId
                LEFT OUTER JOIN DoctorSpecialties AS S ON S.DoctorId = D.Id
                LEFT OUTER JOIN DoctorPositions AS DP ON DP.DoctorId = D.Id
                WHERE (@name IS NULL OR LOWER(P.FamilyName) LIKE LOWER(@name)+'%')
                AND (@specialtyId IS NULL OR S.SpecialtyId = @specialtyId)
                AND (@hospitalId IS NULL OR DP.HospitalId = @hospitalId)
                AND (@departmentId IS NULL OR DP.DepartmentId = @departmentId))";
            migrationBuilder.Sql(function);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string drop =
                @"DROP FUNCTION SearchDoctors";
            migrationBuilder.Sql(drop);
        }
    }
}
