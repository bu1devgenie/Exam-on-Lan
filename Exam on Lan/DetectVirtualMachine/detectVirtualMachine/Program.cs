using System;
using System.Management;

namespace detectVirtualMachine
{
    internal class program
    {

    
    public static bool DetectVirtualMachine()
    {
            using (var searcher = new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem"))
                {
                    using (var items = searcher.Get())
                    {
                        foreach (var item in items)
                        {
                            string manufacturer = item["Manufacturer"].ToString().ToLower();
                            if ((manufacturer == "microsoft corporation" && item["Model"].ToString().ToUpperInvariant().Contains("VIRTUAL"))
                                || manufacturer.Contains("vmware")
                                || item["Model"].ToString() == "VirtualBox")
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
    static void Main(string[] args)
    {
            if (DetectVirtualMachine())
            {
                Console.WriteLine("Run in Virtual Machine");
            }
            else
            {
                Console.WriteLine();
            }

       

    }
    }
}
