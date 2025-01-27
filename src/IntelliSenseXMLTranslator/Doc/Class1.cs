using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntellisenseXMLLocalizer.M
{

   
    // メモ: 生成されたコードは、少なくとも .NET Framework 4.5または .NET Core/Standard 2.0 が必要な可能性があります。

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class doc
    {
        public docAssembly assembly { get; set; }


        [System.Xml.Serialization.XmlArrayItemAttribute("member", IsNullable = false)]
        public docMember[] members { get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docAssembly
    {
        public string name { get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMember
    {
        [System.Xml.Serialization.XmlElementAttribute("exception", typeof(docMemberException))]
        [System.Xml.Serialization.XmlElementAttribute("param", typeof(docMemberParam))]
        [System.Xml.Serialization.XmlElementAttribute("returns", typeof(docMemberReturns))]
        [System.Xml.Serialization.XmlElementAttribute("summary", typeof(docMemberSummary))]
        [System.Xml.Serialization.XmlElementAttribute("typeparam", typeof(docMemberTypeparam))]
        public object[] Items { get; set; }
    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name { get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberException
    {
        [System.Xml.Serialization.XmlElementAttribute("c", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("code", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("para", typeof(docMemberExceptionPara))]
        [System.Xml.Serialization.XmlElementAttribute("paramref", typeof(docMemberExceptionParamref))]
        [System.Xml.Serialization.XmlElementAttribute("see", typeof(docMemberExceptionSee))]
        [System.Xml.Serialization.XmlElementAttribute("typeparamref", typeof(docMemberExceptionTypeparamref))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items { get; set; }

    
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType1[] ItemsElementName { get; set; }

    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text { get; set; }
    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cref { get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberExceptionPara
    {
        public docMemberExceptionParaParamref paramref { get; set; }
    
        public docMemberExceptionParaSee see { get; set; }
    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text { get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberExceptionParaParamref
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name { get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberExceptionParaSee
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cref{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberExceptionParamref
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberExceptionSee
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string langword{ get; set; }

    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cref{ get; set; }

    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberExceptionTypeparamref
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema = false)]
    public enum ItemsChoiceType1
    {
        c,
        code,
        para,
        paramref,
        see,
        typeparamref,
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberParam
    {
        [System.Xml.Serialization.XmlElementAttribute("c", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("code", typeof(docMemberParamCode))]
        [System.Xml.Serialization.XmlElementAttribute("em", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("paramref", typeof(docMemberParamParamref))]
        [System.Xml.Serialization.XmlElementAttribute("see", typeof(docMemberParamSee))]
        [System.Xml.Serialization.XmlElementAttribute("typeparamref", typeof(docMemberParamTypeparamref))]
        [System.Xml.Serialization.XmlElementAttribute("xref", typeof(docMemberParamXref))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items{ get; set; }

    
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType[] ItemsElementName{ get; set; }

    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text{ get; set; }

    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberParamCode
    {
        [System.Xml.Serialization.XmlAttributeAttribute("data-dev-comment-type")]
        public string datadevcommenttype{ get; set; }

    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberParamParamref
    {

        private string nameField;

    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberParamSee
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cref{ get; set; }

    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string langword{ get; set; }

    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberParamTypeparamref
    {

        private string nameField;

    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberParamXref
    {
        [System.Xml.Serialization.XmlAttributeAttribute("data-throw-if-not-resolved")]
        public bool datathrowifnotresolved{ get; set; }

    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string uid{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema = false)]
    public enum ItemsChoiceType
    {
        c,
        code,
        em,
        paramref,
        see,
        typeparamref,
        xref,
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturns
    {
        [System.Xml.Serialization.XmlElementAttribute("br", typeof(object))]
        [System.Xml.Serialization.XmlElementAttribute("c", typeof(docMemberReturnsC))]
        [System.Xml.Serialization.XmlElementAttribute("code", typeof(docMemberReturnsCode))]
        [System.Xml.Serialization.XmlElementAttribute("list", typeof(docMemberReturnsList))]
        [System.Xml.Serialization.XmlElementAttribute("p", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("para", typeof(docMemberReturnsPara))]
        [System.Xml.Serialization.XmlElementAttribute("paramref", typeof(docMemberReturnsParamref))]
        [System.Xml.Serialization.XmlElementAttribute("see", typeof(docMemberReturnsSee))]
        [System.Xml.Serialization.XmlElementAttribute("table", typeof(docMemberReturnsTable))]
        [System.Xml.Serialization.XmlElementAttribute("typeparamref", typeof(docMemberReturnsTypeparamref))]
        [System.Xml.Serialization.XmlElementAttribute("xref", typeof(docMemberReturnsXref))]
        public object[] Items{ get; set; }

    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsC
    {

        public string sub{ get; set; }
    
        public docMemberReturnsCParamref paramref{ get; set; }
    
        public docMemberReturnsCSup sup{ get; set; }
    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsCParamref
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsCSup
    {
        public docMemberReturnsCSupParamref paramref{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsCSupParamref
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsCode
    {
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text{ get; set; }

    
        [System.Xml.Serialization.XmlAttributeAttribute("data-dev-comment-type")]
        public string datadevcommenttype{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsCodeCode
    {
        [System.Xml.Serialization.XmlAttributeAttribute("data-dev-comment-type")]
        public string datadevcommenttype{ get; set; }

    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsList
    {
        public docMemberReturnsListListheader listheader{ get; set; }

    
        [System.Xml.Serialization.XmlElementAttribute("item")]
        public docMemberReturnsListItem[] item{ get; set; }

    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsListListheader
    {

        private docMemberReturnsListListheaderTerm termField;

        private docMemberReturnsListListheaderDescription[] descriptionField;

    
        public docMemberReturnsListListheaderTerm term
        {
            get
            {
                return this.termField;
            }
            set
            {
                this.termField = value;
            }
        }

    
        [System.Xml.Serialization.XmlElementAttribute("description")]
        public docMemberReturnsListListheaderDescription[] description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsListListheaderTerm
    {

        private docMemberReturnsListListheaderTermParamref paramrefField;

        private string[] textField;

    
        public docMemberReturnsListListheaderTermParamref paramref
        {
            get
            {
                return this.paramrefField;
            }
            set
            {
                this.paramrefField = value;
            }
        }

    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsListListheaderTermParamref
    {

        private string nameField;

    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsListListheaderDescription
    {

        private docMemberReturnsListListheaderDescriptionParamref paramrefField;

        private string[] textField;

    
        public docMemberReturnsListListheaderDescriptionParamref paramref
        {
            get
            {
                return this.paramrefField;
            }
            set
            {
                this.paramrefField = value;
            }
        }

    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsListListheaderDescriptionParamref
    {

        private string nameField;

    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsListItem
    {

        private docMemberReturnsListItemTerm termField;

        private docMemberReturnsListItemDescription[] descriptionField;

    
        public docMemberReturnsListItemTerm term
        {
            get
            {
                return this.termField;
            }
            set
            {
                this.termField = value;
            }
        }

    
        [System.Xml.Serialization.XmlElementAttribute("description")]
        public docMemberReturnsListItemDescription[] description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsListItemTerm
    {

        private docMemberReturnsListItemTermParamref paramrefField;

        private docMemberReturnsListItemTermSee[] seeField;

        private string[] textField;

    
        public docMemberReturnsListItemTermParamref paramref
        {
            get
            {
                return this.paramrefField;
            }
            set
            {
                this.paramrefField = value;
            }
        }

    
        [System.Xml.Serialization.XmlElementAttribute("see")]
        public docMemberReturnsListItemTermSee[] see
        {
            get
            {
                return this.seeField;
            }
            set
            {
                this.seeField = value;
            }
        }

    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsListItemTermParamref
    {

        private string nameField;

    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsListItemTermSee
    {

        private string langwordField;

        private string crefField;

    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string langword
        {
            get
            {
                return this.langwordField;
            }
            set
            {
                this.langwordField = value;
            }
        }

    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cref
        {
            get
            {
                return this.crefField;
            }
            set
            {
                this.crefField = value;
            }
        }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsListItemDescription
    {

        private object[] itemsField;

        private string[] textField;

    
        [System.Xml.Serialization.XmlElementAttribute("paramref", typeof(docMemberReturnsListItemDescriptionParamref))]
        [System.Xml.Serialization.XmlElementAttribute("see", typeof(docMemberReturnsListItemDescriptionSee))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsListItemDescriptionParamref
    {

        private string nameField;

    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsListItemDescriptionSee
    {

        private string langwordField;

        private string crefField;

        private string valueField;

    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string langword
        {
            get
            {
                return this.langwordField;
            }
            set
            {
                this.langwordField = value;
            }
        }

    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cref
        {
            get
            {
                return this.crefField;
            }
            set
            {
                this.crefField = value;
            }
        }

    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsPara
    {

        private docMemberReturnsParaSee[] seeField;

        private docMemberReturnsParaParamref[] paramrefField;

        private string[] textField;

    
        [System.Xml.Serialization.XmlElementAttribute("see")]
        public docMemberReturnsParaSee[] see
        {
            get
            {
                return this.seeField;
            }
            set
            {
                this.seeField = value;
            }
        }

    
        [System.Xml.Serialization.XmlElementAttribute("paramref")]
        public docMemberReturnsParaParamref[] paramref
        {
            get
            {
                return this.paramrefField;
            }
            set
            {
                this.paramrefField = value;
            }
        }

    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsParaSee
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cref{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsParaParamref
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsParamref
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsSee
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string langword{ get; set; }

    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cref{ get; set; }

    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsTable
    {
        public docMemberReturnsTableThead thead{ get; set; }

    
        [System.Xml.Serialization.XmlArrayItemAttribute("tr", IsNullable = false)]
        [System.Xml.Serialization.XmlArrayItemAttribute("td", IsNullable = false, NestingLevel = 1)]
        public docMemberReturnsTableTRTD[][] tbody{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsTableThead
    {
        [System.Xml.Serialization.XmlArrayItemAttribute("th", IsNullable = false)]
        public string[] tr{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsTableTRTD
    {
        [System.Xml.Serialization.XmlElementAttribute("code")]
        public docMemberReturnsTableTRTDCode[] code{ get; set; }

    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsTableTRTDCode
    {
        [System.Xml.Serialization.XmlAttributeAttribute("data-dev-comment-type")]
        public string datadevcommenttype{ get; set; }

    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsTypeparamref
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberReturnsXref
    {
        [System.Xml.Serialization.XmlAttributeAttribute("data-throw-if-not-resolved")]
        public bool datathrowifnotresolved{ get; set; }

    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string uid{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberSummary
    {
        [System.Xml.Serialization.XmlElementAttribute("br", typeof(object))]
        [System.Xml.Serialization.XmlElementAttribute("c", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("code", typeof(docMemberSummaryCode))]
        [System.Xml.Serialization.XmlElementAttribute("para", typeof(docMemberSummaryPara))]
        [System.Xml.Serialization.XmlElementAttribute("paramref", typeof(docMemberSummaryParamref))]
        [System.Xml.Serialization.XmlElementAttribute("see", typeof(docMemberSummarySee))]
        [System.Xml.Serialization.XmlElementAttribute("typeparamref", typeof(docMemberSummaryTypeparamref))]
        [System.Xml.Serialization.XmlElementAttribute("xref", typeof(docMemberSummaryXref))]
        public object[] Items{ get; set; }

    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberSummaryCode
    {
        [System.Xml.Serialization.XmlAttributeAttribute("data-dev-comment-type")]
        public string datadevcommenttype{ get; set; }

    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberSummaryPara
    {
        [System.Xml.Serialization.XmlElementAttribute("c")]
        public string[] c{ get; set; }

        [System.Xml.Serialization.XmlElementAttribute("see")]
        public docMemberSummaryParaSee[] see{ get; set; }
    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberSummaryParaSee
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cref{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberSummaryParamref
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberSummarySee
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cref{ get; set; }
    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string langword{ get; set; }

    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberSummaryTypeparamref
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberSummaryXref
    {
        [System.Xml.Serialization.XmlAttributeAttribute("data-throw-if-not-resolved")]
        public bool datathrowifnotresolved{ get; set; }

    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string uid{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberTypeparam
    {
        [System.Xml.Serialization.XmlElementAttribute("c", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("code", typeof(docMemberTypeparamCode))]
        [System.Xml.Serialization.XmlElementAttribute("paramref", typeof(docMemberTypeparamParamref))]
        [System.Xml.Serialization.XmlElementAttribute("see", typeof(docMemberTypeparamSee))]
        [System.Xml.Serialization.XmlElementAttribute("typeparamref", typeof(docMemberTypeparamTypeparamref))]
        public object[] Items{ get; set; }

    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text{ get; set; }

    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberTypeparamCode
    {
        [System.Xml.Serialization.XmlAttributeAttribute("data-dev-comment-type")]
        public string datadevcommenttype{ get; set; }
    
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberTypeparamParamref
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberTypeparamSee
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cref{ get; set; }

    
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string langword{ get; set; }
    }


    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class docMemberTypeparamTypeparamref
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name{ get; set; }
    }




}
