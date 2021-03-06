-- Autogenerated by MSIL2Ada v. 2
-- By: Martin C. Carlisle
--     Department of Computer Science
--     US Air Force Academy
--     carlislem@acm.org
with MSSyst.Object;
with MSIL_Types;
use MSIL_Types;
limited with MSSyst.String;
limited with MSSyst.Type_k;
package raptor.Registry_Settings is
   type Typ is new MSSyst.Object.Typ   with record
      null;
   end record;
   Ignore_Updates : Standard.Boolean;
   pragma Import(MSIL,Ignore_Updates,"Ignore_Updates");
   type Ref is access all Typ'Class;
   type Ref_addrof is access all Ref;
   type Ref_Arr is array(Natural range <>) of Ref;
   type Ref_Array is access all Ref_Arr;
   type Ref_Array_addrof is access all Ref_Array;
   function new_Registry_Settings(
      This : Ref := null) return Ref;
   function Global_Read(
      key : access MSSyst.String.Typ'Class) return access MSSyst.String.Typ'Class;
   function Read(
      key : access MSSyst.String.Typ'Class) return access MSSyst.String.Typ'Class;
   procedure Write(
      key : access MSSyst.String.Typ'Class;
      val : access MSSyst.String.Typ'Class);
private
   pragma Convention(MSIL,Typ);
   pragma MSIL_Constructor(new_Registry_Settings);
   pragma Import(MSIL,Global_Read,"Global_Read");
   pragma Import(MSIL,Read,"Read");
   pragma Import(MSIL,Write,"Write");
end raptor.Registry_Settings;
pragma Import(MSIL,raptor.Registry_Settings,
   ".ver 4:0:5:2",
   "[raptor]raptor.Registry_Settings");
