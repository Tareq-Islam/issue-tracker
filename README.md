--
Run following command in API folder
--
dotnet ef dbcontext scaffold "Server=localhost; Database=IssueTrackerDB; Trusted_Connection=True; Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer --context IssueTrackerApplicationDbContext --context-namespace Infrastructure.Context --context-dir 
../Infrastructure/Context -f --output-dir ../Domain/Entities --namespace Domain.Entities --no-onconfiguring
