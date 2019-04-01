
dotnet ef dbcontext scaffold "Data Source={server};Database={database-name};Persist Security Info=True;User ID={username};Password={password};" Microsoft.EntityFrameworkCore.SqlServer -o Entities -c d

dotnet ef dbcontext scaffold "Data Source=REXRMASSII002;Database=RMSYNCHRO;Persist Security Info=True;User ID=RMsynchro;Password=q&%d78IRGyF(;" Microsoft.EntityFrameworkCore.SqlServer -o Entities -c RxDbContext -f

Scaffold-DbContext -Connection "Server=(LocalDb)\MSSqlLocalDb;Database=Database1;Trusted_Connection=True;" -Provider "Microsoft.EntityFrameworkCore.SqlServer" -DataAnnotations


 //Paramtere à renseigner ==> appsettings.json

"ConnectionSettings": {
    "Server": "HBOYOU\\DEVSERVER2012",
    "Database": "MachineDB",
    "Username": "sa",
    "Password": "P@ssw0rd"
  },

