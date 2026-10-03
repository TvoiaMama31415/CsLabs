
string[] lines = File.ReadAllLines("../../../../event_server.log");
foreach (string line in lines)
{
    DateTime dt = GetLogDateTime(line);
    String lvl = GetLogLevel(line);
    String tupe = GetLogTupe(line);
    String txt = GetLogTxt(line);
    Console.WriteLine($"{dt} {lvl} {tupe} {txt}");
    break;
}
static DateTime GetLogDateTime(string line)
{
    int ind1 = line.IndexOf(" ");
    int ind2 = line.IndexOf(" ", ind1 + 1);
    string txt = line.Substring(0, ind2);
    DateTime date = DateTime.Parse(txt);
    return date;
}
static String GetLogLevel(string line)
{
    int ind1 = line.IndexOf("[");
    int ind2 = line.IndexOf("]", ind1);
    string lvl = line.Substring(ind1 + 1, ind2 - ind1 - 1);
    return lvl;
}
static String GetLogTupe(string line)
{
    int ind1 = line.IndexOf('[');
    int ind11 = line.IndexOf('[', ind1 + 1);
    int ind2 = line.IndexOf(']');
    int ind22 = line.IndexOf(']', ind2 + 1);
    string tupi = line.Substring(ind11 + 1, ind22 - ind11 - 1);
    return tupi;
}
static String GetLogTxt(string line)
{
    int ind1=line.IndexOf("]");
    int ind2 = line.IndexOf(']', ind1 + 1);
    int ind3 = line.IndexOf(' ', ind2);
    string txt = line.Substring(ind3+1);
    return txt;
}

