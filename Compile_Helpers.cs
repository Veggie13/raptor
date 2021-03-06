using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

namespace raptor
{
	/// <summary>
	/// Summary description for Compile_Helpers.
	/// </summary>
    public class Compile_Helpers
    {
        public Compile_Helpers()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private static System.Collections.Hashtable declarations = new System.Collections.Hashtable();

        // returns true if already declared
        public static bool Start_New_Declaration(string name)
        {
            bool result;
            if (name.ToLower().Equals("this") || name.ToLower().Equals("super"))
                return true;
            result = declarations.ContainsKey(name.ToLower());
            if (!result)
            {
                declarations.Add(name.ToLower(), null);
            }
            return result;
        }
        public static void Do_Compilation(Oval start, generate_interface.typ gil,
            TabControl.TabPageCollection tpc)
        {
            if (Component.Current_Mode != Mode.Expert)
            {
                Do_Compilation_Imperative(start, gil as GeneratorAda.Imperative_Interface, tpc);
            }
            else
            {
                try
                {
                    Do_Compilation_OO(start, gil as GeneratorAda.OO_Interface, tpc);
                }
                catch
                {
                    (gil as GeneratorAda.OO_Interface).abort();
                    throw;
                }
            }
        }
        private static void Do_Compilation_OO(Oval start, GeneratorAda.OO_Interface gil, TabControl.TabPageCollection tpc)
        {
            _tpc = tpc;
            foreach (NClass.Core.IEntity ie in Runtime.parent.projectCore.Entities)
            {
                if (ie is NClass.Core.InterfaceType)
                {
                    gil.start_interface(ie as NClass.Core.InterfaceType);
                    foreach (NClass.Core.Operation o in
                        (ie as NClass.Core.InterfaceType).Operations)
                    {
                        if (o is NClass.Core.Method)
                        {
                            gil.declare_interface_method(o as NClass.Core.Method);
                        }
                    }
                    gil.done_interface(ie as NClass.Core.InterfaceType);
                }
            }
            foreach (ClassTabPage ctp in allClasses(tpc))
            {
                gil.declare_class(ctp.ct);
                foreach (Procedure_Chart pc in allMethods(ctp))
                {
                    NClass.Core.Method method = pc.method;
                    gil.declare_method(method);
                }
            }
            foreach (ClassTabPage ctp in allClasses(tpc))
            {
                NClass.Core.ClassType ct = ctp.ct;
                gil.start_class(ct);
                foreach (NClass.Core.Field f in ct.Fields)
                {
                    gil.declare_field(f);
                }
                    
                foreach (NClass.Core.Operation o in ct.Operations)
                {
                    if ((o is NClass.Core.Method) &&
                        o.IsAbstract)
                    {
                        gil.declare_abstract_method(o as NClass.Core.Method);
                    }
                }
                foreach (Procedure_Chart pc in allMethods(ctp))
                {
                    NClass.Core.Method method = pc.method;
                    gil.start_method(method);
                    declarations.Clear();
                    pc.Start.compile_pass1(gil);
                    gil.Done_Variable_Declarations();
                    pc.Start.Emit_Code(gil);
                    gil.Done_Method();
                }
                gil.done_class(ctp.ct);
            }

            gil.Start_Method("Main");
            declarations.Clear();
            start.compile_pass1(gil);
            gil.Done_Variable_Declarations();
            start.Emit_Code(gil);
            gil.Done_Method();
            gil.Finish();
        }
        private static void Do_Compilation_Imperative(Oval start, 
            GeneratorAda.Imperative_Interface gil, TabControl.TabPageCollection tpc)
        {
            _tpc = tpc;
            for (int i = 1; i < tpc.Count; i++)
            {
                Procedure_Chart pc;
                if (tpc[i] is Procedure_Chart)
                {
                    pc = tpc[i] as Procedure_Chart;
                    gil.Declare_Procedure(pc.Text.Trim(),
                        pc.getArgs(), pc.getArgIsInput(), pc.getArgIsOutput());
                }
            }


            // Use the utility method to generate the IL instructions that print a string to the console.

            // emit methods for procedures
            for (int i = 1; i < tpc.Count; i++)
            {
                Procedure_Chart pc;
                if (tpc[i] is Procedure_Chart)
                {
                    pc = tpc[i] as Procedure_Chart;
                    gil.Start_Method(pc.Text);
                    declarations.Clear();
                    pc.Start.compile_pass1(gil);
                    gil.Done_Variable_Declarations();
                    pc.Start.Emit_Code(gil);
                    gil.Done_Method();
                }
            }

            gil.Start_Method("Main");
            declarations.Clear();
            start.compile_pass1(gil);
            gil.Done_Variable_Declarations();
            start.Emit_Code(gil);
            gil.Done_Method();
            gil.Finish();


        }
        private static TabControl.TabPageCollection _tpc;
        public static TabControl.TabPageCollection get_tpc()
        {
            return _tpc;
        }

        public static IEnumerable<Subchart> allSubcharts(System.Windows.Forms.TabControl.TabPageCollection tabpages)
        {
            foreach (TabPage tp in tabpages)
            {
                if (tp is Subchart)
                {
                    yield return tp as Subchart;
                }
                else if (tp is ClassTabPage)
                {
                    ClassTabPage ctp = tp as ClassTabPage;
                    for (int j = 0; j < ctp.tabControl1.TabPages.Count; j++)
                    {
                        yield return ctp.tabControl1.TabPages[j] as Subchart;
                    }
                }
            }
        }

        public static IEnumerable<ClassTabPage> allClasses(System.Windows.Forms.TabControl.TabPageCollection tabpages)
        {
            foreach (TabPage tp in tabpages)
            {
                if (tp is ClassTabPage)
                {
                    yield return tp as ClassTabPage;
                }
            }
        }

        public static IEnumerable<Procedure_Chart> allMethods(ClassTabPage ctp)
        {
            foreach (TabPage tp in ctp.tabControl1.TabPages)
            {
                if (tp is Procedure_Chart)
                {
                    yield return tp as Procedure_Chart;
                }
            }
        }
        public static Subchart mainSubchart(System.Windows.Forms.TabControl.TabPageCollection tabpages)
        {
            if (Component.Current_Mode == Mode.Expert)
            {
                return (Subchart)tabpages[1];
            }
            else
            {
                return (Subchart)tabpages[0];
            }
        }

        public static void Compile_Flowchart(System.Windows.Forms.TabControl.TabPageCollection tabpages)
        {
            _tpc = tabpages;
            Oval start = mainSubchart(tabpages).Start;
            foreach (Subchart sbchrt in allSubcharts(tabpages)) 
            {
                sbchrt.am_compiling = false;
            }
            mainSubchart(tabpages).am_compiling = true;
            Generate_IL gil = new Generate_IL("MyAssembly");
            try
            {
                Do_Compilation(start, gil, tabpages);
            }
            catch
            {
                foreach (Subchart sbchrt in allSubcharts(tabpages))
                {
                    sbchrt.am_compiling = false;
                }
                throw;
            }
            mainSubchart(tabpages).am_compiling = false;
        }
        public static void Compile_Flowchart_To(Oval start, string directory, string filename)
        {
            Generate_IL gil = new Generate_IL(
                System.IO.Path.GetFileNameWithoutExtension(filename));
            Do_Compilation(start, gil, Runtime.parent.carlisle.TabPages);
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }
            string app_dir = System.IO.Path.GetDirectoryName(
                System.Windows.Forms.Application.ExecutablePath);
            System.IO.File.Copy(
               System.Windows.Forms.Application.ExecutablePath,
               System.IO.Path.Combine(directory, "raptor.dll"), true);
            System.IO.File.Copy(
               System.IO.Path.Combine(app_dir, "interpreter.dll"),
               System.IO.Path.Combine(directory, "interpreter.dll"), true);
            System.IO.File.Copy(
               System.IO.Path.Combine(app_dir, "dotnetgraph.dll"),
               System.IO.Path.Combine(directory, "dotnetgraph.dll"), true);
            System.IO.File.Copy(
               System.IO.Path.Combine(app_dir, "mgnat.dll"),
               System.IO.Path.Combine(directory, "mgnat.dll"), true);
            System.IO.File.Copy(
               System.IO.Path.Combine(app_dir, "mgnatcs.dll"),
               System.IO.Path.Combine(directory, "mgnatcs.dll"), true);
            string[] plugins = Plugins.Get_Plugin_List();
            string[] assemblies = Plugins.Get_Assembly_Names();
            for (int j = 0; j < plugins.Length; j++)
            {
                System.IO.File.Copy(
                   plugins[j],
                   System.IO.Path.Combine(directory,
                      assemblies[j]) + ".dll", true);
            }
            if (System.IO.File.Exists(System.IO.Path.Combine(directory, filename)))
            {
                System.IO.File.Delete(System.IO.Path.Combine(directory, filename));
            }
            gil.Save_Result(filename);
            System.IO.File.Move(filename,
                System.IO.Path.Combine(directory, filename));
        }
        static bool from_commandline;
        public static void runCompiledHelper()
        {
            Assembly[] myAssemblies = Thread.GetDomain().GetAssemblies();
            MethodInfo mi = null;
            // Get the dynamic assembly named 'MyAssembly'. 
            for (int i = 0; i < myAssemblies.Length; i++)
            {
                if (myAssemblies[i].GetName().Name == "MyAssembly")
                {
                    MethodInfo new_mi;
                    try
                    {
                        new_mi = myAssemblies[i].GetType("MyType").GetMethod("Main");
                        mi = new_mi;
                    }
                    catch
                    {
                        if (i == myAssemblies.Length - 1)
                        {
                            throw;
                        }
                    }
                }
            }
            if (mi != null)
            {
                try
                {
                    mi.Invoke(null, null);
                }
                catch (System.Threading.ThreadAbortException f)
                {
                    Runtime.consoleWriteln("----compiled run aborted----");
                }
                catch (System.Exception e)
                {
                    // added 3/2/05 by mcc
                    if (raptor_files_pkg.output_redirected() && from_commandline)
                    {
                        raptor_files_pkg.writeln("exception occurred! flowchart terminated abnormally\n"
                            + e.Message);
                        raptor_files_pkg.stop_redirect_output();
                    }
                    else
                    {
                        if (e.InnerException != null)
                        {
                            Runtime.consoleWrite("Exception! " + e.InnerException.Message + e.InnerException.StackTrace);
                        }
                        else
                        {
                            Runtime.consoleWrite("Exception! " + e.Message + e.StackTrace);
                        }
                    }
                }
            }
        }
        public static System.Threading.Thread run_compiled_thread;
        public static void Run_Compiled_NoThread(bool was_from_commandline)
        {
            from_commandline = was_from_commandline;
            runCompiledHelper();
        }
        public static void Run_Compiled(bool was_from_commandline)
        {
            from_commandline = was_from_commandline;
            if (run_compiled_thread != null && run_compiled_thread.ThreadState == ThreadState.Running)
            {
                run_compiled_thread.Abort();
            }
            run_compiled_thread = new Thread(new ThreadStart(runCompiledHelper));
            run_compiled_thread.Start();
        }
    }
}
