-- Autogenerated by MSIL2Ada v. 2
-- By: Martin C. Carlisle
--     Department of Computer Science
--     US Air Force Academy
--     carlislem@acm.org
with MSSyst.Object;
with MSIL_Types;
use MSIL_Types;
limited with NClass.Core.ClassType;
limited with NClass.Core.Field;
limited with NClass.Core.InterfaceType;
limited with NClass.Core.Method;
with generate_interface.typ;
with generate_interface_oo.typ;
package GeneratorAda.OO_Interface is
   type Typ is interface;
   type Ref is access all Typ'Class;
   type Ref_addrof is access all Ref;
   type Ref_Arr is array(Natural range <>) of Ref;
   type Ref_Array is access all Ref_Arr;
   type Ref_Array_addrof is access all Ref_Array;
   procedure abort_k(
      This : access Typ) is abstract;
   procedure declare_abstract_method(
      This : access Typ;
      method : access NClass.Core.Method.Typ'Class) is abstract;
   procedure declare_class(
      This : access Typ;
      ct : access NClass.Core.ClassType.Typ'Class) is abstract;
   procedure declare_field(
      This : access Typ;
      f : access NClass.Core.Field.Typ'Class) is abstract;
   procedure declare_interface_method(
      This : access Typ;
      method : access NClass.Core.Method.Typ'Class) is abstract;
   procedure declare_method(
      This : access Typ;
      method : access NClass.Core.Method.Typ'Class) is abstract;
   procedure done_class(
      This : access Typ;
      ct : access NClass.Core.ClassType.Typ'Class) is abstract;
   procedure done_interface(
      This : access Typ;
      i : access NClass.Core.InterfaceType.Typ'Class) is abstract;
   procedure Done_Method(
      This : access Typ) is abstract;
   procedure start_class(
      This : access Typ;
      ct : access NClass.Core.ClassType.Typ'Class) is abstract;
   procedure start_interface(
      This : access Typ;
      i : access NClass.Core.InterfaceType.Typ'Class) is abstract;
   procedure start_method(
      This : access Typ;
      method : access NClass.Core.Method.Typ'Class) is abstract;
private
   pragma Import(MSIL,abort_k,"abort");
   pragma Import(MSIL,declare_abstract_method,"declare_abstract_method");
   pragma Import(MSIL,declare_class,"declare_class");
   pragma Import(MSIL,declare_field,"declare_field");
   pragma Import(MSIL,declare_interface_method,"declare_interface_method");
   pragma Import(MSIL,declare_method,"declare_method");
   pragma Import(MSIL,done_class,"done_class");
   pragma Import(MSIL,done_interface,"done_interface");
   pragma Import(MSIL,Done_Method,"Done_Method");
   pragma Import(MSIL,start_class,"start_class");
   pragma Import(MSIL,start_interface,"start_interface");
   pragma Import(MSIL,start_method,"start_method");
end GeneratorAda.OO_Interface;
pragma Import(MSIL,GeneratorAda.OO_Interface,
   ".ver 1:0:0:0",
   "[GeneratorAda]GeneratorAda.OO_Interface");