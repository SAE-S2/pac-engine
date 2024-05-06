using MySqlConnector;
using Renci.SshNet;

static (SshClient SshClient, uint Port) ConnectSsh(string sshHostName, string sshUserName, string sshPassword = null,
    string sshKeyFile = null, string sshPassPhrase = null, int sshPort = 22, string databaseServer = "localhost", int databasePort = 3306)
{
    // check arguments
    if (string.IsNullOrEmpty(sshHostName))
        throw new ArgumentException($"{nameof(sshHostName)} must be specified.", nameof(sshHostName));
    if (string.IsNullOrEmpty(sshHostName))
        throw new ArgumentException($"{nameof(sshUserName)} must be specified.", nameof(sshUserName));
    if (string.IsNullOrEmpty(sshPassword) && string.IsNullOrEmpty(sshKeyFile))
        throw new ArgumentException($"One of {nameof(sshPassword)} and {nameof(sshKeyFile)} must be specified.");
    if (string.IsNullOrEmpty(databaseServer))
        throw new ArgumentException($"{nameof(databaseServer)} must be specified.", nameof(databaseServer));

    // define the authentication methods to use (in order)
    var authenticationMethods = new List<AuthenticationMethod>();
    if (!string.IsNullOrEmpty(sshKeyFile))
    {
        authenticationMethods.Add(new PrivateKeyAuthenticationMethod(sshUserName,
            new PrivateKeyFile(sshKeyFile, string.IsNullOrEmpty(sshPassPhrase) ? null : sshPassPhrase)));
    }
    if (!string.IsNullOrEmpty(sshPassword))
    {
        authenticationMethods.Add(new PasswordAuthenticationMethod(sshUserName, sshPassword));
    }

    // connect to the SSH server
    var sshClient = new SshClient(new ConnectionInfo(sshHostName, sshPort, sshUserName, authenticationMethods.ToArray()));
    sshClient.Connect();

    // forward a local port to the database server and port, using the SSH server
    var forwardedPort = new ForwardedPortLocal("127.0.0.1", databaseServer, (uint)databasePort);
    sshClient.AddForwardedPort(forwardedPort);
    forwardedPort.Start();

    return (sshClient, forwardedPort.BoundPort);
}

var sshServer = "your ssh server";
var sshUserName = "your SSH user name";
var sshPassword = "your SSH password";
var databaseServer = "your database server";
var databaseUserName = "your database user name";
var databasePassword = "your database password";

var (sshClient, localPort) = ConnectSsh(sshServer, sshUserName, sshPassword, databaseServer: databaseServer);
using (sshClient)
{
    MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder
    {
        Server = "127.0.0.1",
        Port = localPort,
        UserID = databaseUserName,
        Password = databasePassword,
    };

    using var connection = new MySqlConnection(csb.ConnectionString);
    connection.Open();
}
