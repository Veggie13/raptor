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
with raptor.Component;
package raptor.Component.FootPrint is
   type Typ is new MSSyst.Object.Typ   with record
      left : Integer;
      pragma Import(MSIL,left,"left");
      right : Integer;
      pragma Import(MSIL,right,"right");
      height : Integer;
      pragma Import(MSIL,height,"height");
   end record;
   type Ref is access all Typ'Class;
   type Ref_addrof is access all Ref;
   type Ref_Arr is array(Natural range <>) of Ref;
   type Ref_Array is access all Ref_Arr;
   type Ref_Array_addrof is access all Ref_Array;
   function new_FootPrint(
      This : Ref := null) return Ref;
   function Clone(
      This : access Typ) return access raptor.Component.FootPrint.Typ'Class;
private
   pragma Convention(MSIL,Typ);
   pragma MSIL_Constructor(new_FootPrint);
   pragma Import(MSIL,Clone,"Clone");
end raptor.Component.FootPrint;
pragma Import(MSIL,raptor.Component.FootPrint,
   ".ver 4:0:5:2",
   "[raptor]raptor.Component/FootPrint");
