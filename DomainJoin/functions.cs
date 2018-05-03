using System.Management;

namespace DomainJoin

{
    class functions
    {
        /// <summary>
        /// Set Machine Name
        /// </summary>
        public static bool SetMachineName(string newName)
        {
            //_lh.Log(LogHandler.LogType.Debug, string.Format("Setting Machine Name to '{0}'...", newName));

            // Invoke WMI to populate the machine name
            using (ManagementObject wmiObject = new ManagementObject(new ManagementPath("Win32_ComputerSystem.Name='" + System.Environment.MachineName + "'")))
            {
                ManagementBaseObject inputArgs = wmiObject.GetMethodParameters("Rename");
                inputArgs["Name"] = newName;

                // Set the name
                ManagementBaseObject outParams = wmiObject.InvokeMethod("Rename", inputArgs, null);

                // Weird WMI shennanigans to get a return code (is there no better way to do this??)
                uint ret = (uint)(outParams.Properties["ReturnValue"].Value);
                if (ret == 0)
                {
                    // It worked
                    return true;

                }
                else
                {
                    // It didn't work
                   // _lh.Log(LogHandler.LogType.Fatal, string.Format("Unable to change Machine Name from '{0}' to '{1}'", System.Environment.MachineName, newName));
                    return false;
                }
            }
        }

        /// <summary>
        /// Set domain membership
        /// </summary>
        public static bool SetDomainMembership(string strUname,string strPw, string strDom )
        {
//       lh.Log(LogHandler.LogType.Debug, string.Format("Setting domain membership of '{0}' to '{1}'...", System.Environment.MachineName, _targetDomain));

            // Invoke WMI to join the domain
            using (ManagementObject wmiObject = new ManagementObject(new ManagementPath("Win32_ComputerSystem.Name='" + System.Environment.MachineName + "'")))
            {
                try
                {
                    // Obtain in-parameters for the method
                    ManagementBaseObject inParams = wmiObject.GetMethodParameters("JoinDomainOrWorkgroup");
                 
                    inParams["Name"] = strDom;
                    inParams["Password"] = strPw;
                    inParams["UserName"] = strUname;
                    inParams["FJoinOptions"] = 3; // Magic number: 3 = join to domain and create computer account

                    // Execute the method and obtain the return values.
                    ManagementBaseObject outParams = wmiObject.InvokeMethod("JoinDomainOrWorkgroup", inParams, null);
                  //_lh.Log(LogHandler.LogType.Debug, string.Format("JoinDomainOrWorkgroup return code: '{0}'", outParams["ReturnValue"]));

                    // Did it work?  ** disabled so we restart later even if it fails
                    //uint ret = (uint)(outParams.Properties["ReturnValue"].Value);
                    //if (ret != 0)
                    //{
                    //  // Nope
                    //  _lh.Log(LogHandler.LogType.Fatal, string.Format("JoinDomainOrWorkgroup failed with return code: '{0}'", outParams["ReturnValue"]));
                    //  return false;
                    //}

                    return true;
                }
                catch (ManagementException e)
                {
                    // It didn't work
                   // _lh.Log(LogHandler.LogType.Fatal, string.Format("Unable to join domain '{0}'", _targetDomain), e);
                    return false;
                }
            }
        }

    }
}
