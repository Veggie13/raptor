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
limited with numbers.value;
package raptor.Runtime_Helpers is
   type Typ is new MSSyst.Object.Typ   with record
      null;
   end record;
   type Ref is access all Typ'Class;
   type Ref_addrof is access all Ref;
   type Ref_Arr is array(Natural range <>) of Ref;
   type Ref_Array is access all Ref_Arr;
   type Ref_Array_addrof is access all Ref_Array;
   function new_Runtime_Helpers(
      This : Ref := null) return Ref;
   function Get_Value_String(
      s : access numbers.value.Typ'Class;
      value_index : access numbers.value.Typ'Class) return access numbers.value.Typ'Class;
   procedure Set_Value_String(
      s : access numbers.value.Typ'Class;
      value_index : access numbers.value.Typ'Class;
      v : access numbers.value.Typ'Class);
private
   pragma Convention(MSIL,Typ);
   pragma MSIL_Constructor(new_Runtime_Helpers);
   pragma Import(MSIL,Get_Value_String,"Get_Value_String");
   pragma Import(MSIL,Set_Value_String,"Set_Value_String");
end raptor.Runtime_Helpers;
pragma Import(MSIL,raptor.Runtime_Helpers,
   ".ver 4:0:5:2",
   "[raptor]raptor.Runtime_Helpers");
