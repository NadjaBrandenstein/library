# Read and parse .env file
Get-Content .env | ForEach-Object {
    if ($_ -match '^([^#][^=]+)=(.*)$') {
        $name = $matches[1].Trim()
        $value = $matches[2].Trim()
        [Environment]::SetEnvironmentVariable($name, $value, "Process")
    }
}

# Install EF tool
dotnet tool install -g dotnet-ef

# Run scaffolding
dotnet ef dbcontext scaffold "Host=ep-round-silence-agnocc77-pooler.c-2.eu-central-1.aws.neon.tech; Database=neondb; Username=neondb_owner; Password=npg_8zDSkPZ6vinr; SSL Mode=VerifyFull; Channel Binding=Require;" Npgsql.EntityFrameworkCore.PostgreSQL --context MyDbContext --no-onconfiguring --schema library --force

# I know this is not best practice. I tried it the right way and at first it worked and then it didn't, and quite frankly I had better things to do.