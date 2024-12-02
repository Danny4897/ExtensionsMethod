namespace ExtensionMethods.OptionSample.Processes;

public delegate string FormatName(NameType name);
public delegate string FormatNameList(NameType[] names);
public delegate string FormatNameListExt(FormatName nameFormatter, NameType[] names);

public static class FormatNameListExtensions
{
    public static FormatNameList Apply(this FormatNameListExt formatter, FormatName nameFormatter) =>
    names => formatter(nameFormatter, names);
    
    // public static FormatNameList Apply(this FormatNameList formatter, FormatName nameFormatter) =>
    //     names => formatter(names);
}

public static class FormatNameListDefaults
{
    public static FormatNameListExt CommaSeparatedNames => (nameFormatter, names) =>
        string.Join(", ", names.Select(nameFormatter.Invoke));
    
    public static FormatNameListExt ListDisplayedNames => (nameFormatter, names) =>
        string.Join("\n", names.Select(nameFormatter.Invoke));
}

public static class FormatNameDefaults
{
    public static FormatName FormatFullName => (name) =>
        name.Map(
            fullName => $"{fullName.FirstName} {fullName.LastName}",
            mononym => $"{mononym.Name}"
        );

    public static FormatName FormatInitials => (name) =>
        name.Map(
            fullName => $"{fullName.FirstName[..1]} {fullName.LastName[..1]}",
            mononym => $"{mononym.Name[..1]}"
        );


    // public static FormatNameList FormtAuthorsInitial => (names) =>
    //     string.Join("- ", names.Select(name => name.Map(
    //         fullName => $"{fullName.FirstName[..1]} {fullName.LastName[..1]}",
    //         mononym => $"{mononym.Name[..1]}"
    //     )));
}