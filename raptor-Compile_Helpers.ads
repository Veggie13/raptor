-- Autogenerated by MSIL2Ada v. 2
-- By: Martin C. Carlisle
--     Department of Computer Science
--     US Air Force Academy
--     carlislem@acm.org
with MSSyst.Object;
with MSIL_Types;
use MSIL_Types;
limited with MSSyst.String;
limited with MSSyst.Threading.Thread;
limited with MSSyst.Type_k;
limited with MSSyst.Windows.Forms.TabControl.TabPageCollection;
limited with generate_interface;
limited with raptor.Oval;
limited with raptor.Subchart;
package raptor.Compile_Helpers is
   type Typ is new MSSyst.Object.Typ   with record
      null;
   end record;
   run_compiled_thread : access MSSyst.Threading.Thread.Typ'Class;
   pragma Import(MSIL,run_compiled_thread,"run_compiled_thread");
   type Ref is access all Typ'Class;
   type Ref_addrof is access all Ref;
   type Ref_Arr is array(Natural range <>) of Ref;
   type Ref_Array is access all Ref_Arr;
   type Ref_Array_addrof is access all Ref_Array;
   function new_Compile_Helpers(
      This : Ref := null) return Ref;
   procedure Compile_Flowchart(
      tabpages : access MSSyst.Windows.Forms.TabControl.TabPageCollection.Typ'Class);
   procedure Compile_Flowchart_To(
      start : access raptor.Oval.Typ'Class;
      directory : access MSSyst.String.Typ'Class;
      filename : access MSSyst.String.Typ'Class);
   procedure Do_Compilation(
      start : access raptor.Oval.Typ'Class;
      gil : access generate_interface.Typ'Class;
      tpc : access MSSyst.Windows.Forms.TabControl.TabPageCollection.Typ'Class);
   function get_tpc return access MSSyst.Windows.Forms.TabControl.TabPageCollection.Typ'Class;
   function mainSubchart(
      tabpages : access MSSyst.Windows.Forms.TabControl.TabPageCollection.Typ'Class) return access raptor.Subchart.Typ'Class;
   procedure Run_Compiled(
      was_from_commandline : Standard.Boolean);
   procedure Run_Compiled_NoThread(
      was_from_commandline : Standard.Boolean);
   procedure runCompiledHelper;
   function Start_New_Declaration(
      name : access MSSyst.String.Typ'Class) return Standard.Boolean;
private
   pragma Convention(MSIL,Typ);
   pragma MSIL_Constructor(new_Compile_Helpers);
   pragma Import(MSIL,Compile_Flowchart,"Compile_Flowchart");
   pragma Import(MSIL,Compile_Flowchart_To,"Compile_Flowchart_To");
   pragma Import(MSIL,Do_Compilation,"Do_Compilation");
   pragma Import(MSIL,get_tpc,"get_tpc");
   pragma Import(MSIL,mainSubchart,"mainSubchart");
   pragma Import(MSIL,Run_Compiled,"Run_Compiled");
   pragma Import(MSIL,Run_Compiled_NoThread,"Run_Compiled_NoThread");
   pragma Import(MSIL,runCompiledHelper,"runCompiledHelper");
   pragma Import(MSIL,Start_New_Declaration,"Start_New_Declaration");
end raptor.Compile_Helpers;
pragma Import(MSIL,raptor.Compile_Helpers,
   ".ver 4:0:5:2",
   "[raptor]raptor.Compile_Helpers");
