using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Contendio.Sql.Mapping
{
    public class RepositoryMetaType : MetaType
    {
        public MetaModel RepositoryMetaModel { get; private set; }
        public string Workspace { get; private set; }
        public MetaType Original { get; private set; }
        public MetaTable RepositoryMetaTable { get; private set; }

        public RepositoryMetaType(string workspace, MetaType original, MetaTable table, MetaModel metaModel)
        {
            this.Workspace = workspace;
            this.Original = original;
            this.RepositoryMetaTable = table;
            this.RepositoryMetaModel = metaModel;
        }

        public override MetaType GetInheritanceType(Type type)
        {
            return this.Original.GetInheritanceType(type);
        }

        public override MetaType GetTypeForInheritanceCode(object code)
        {
            return this.Original.GetTypeForInheritanceCode(code);
        }

        public override MetaDataMember GetDataMember(MemberInfo member)
        {
            return this.Original.GetDataMember(member);
        }

        public override MetaModel Model
        {
            get { return this.RepositoryMetaModel; }
        }

        public override MetaTable Table
        {
            get { return this.RepositoryMetaTable; }
        }

        public override Type Type
        {
            get { return this.Original.Type; }
        }

        public override string Name
        {
            get { return this.Original.Name; }
        }

        public override bool IsEntity
        {
            get { return this.Original.IsEntity; }
        }

        public override bool CanInstantiate
        {
            get { return this.Original.CanInstantiate; }
        }

        public override MetaDataMember DBGeneratedIdentityMember
        {
            get { return this.Original.DBGeneratedIdentityMember; }
        }

        public override MetaDataMember VersionMember
        {
            get { return this.Original.VersionMember; }
        }

        public override MetaDataMember Discriminator
        {
            get { return this.Original.Discriminator; }
        }

        public override bool HasUpdateCheck
        {
            get { return this.Original.HasUpdateCheck; }
        }

        public override bool HasInheritance
        {
            get { return this.Original.HasInheritance; }
        }

        public override bool HasInheritanceCode
        {
            get { return this.Original.HasInheritanceCode; }
        }

        public override object InheritanceCode
        {
            get { return this.Original.InheritanceCode; }
        }

        public override bool IsInheritanceDefault
        {
            get { return this.Original.IsInheritanceDefault; }
        }

        public override MetaType InheritanceRoot
        {
            get { return this.Original.InheritanceRoot; }
        }

        public override MetaType InheritanceBase
        {
            get { return this.Original.InheritanceBase; }
        }

        public override MetaType InheritanceDefault
        {
            get { return this.Original.InheritanceDefault; }
        }

        public override ReadOnlyCollection<MetaType> InheritanceTypes
        {
            get { return this.Original.InheritanceTypes; }
        }

        public override bool HasAnyLoadMethod
        {
            get { return this.Original.HasAnyLoadMethod; }
        }

        public override bool HasAnyValidateMethod
        {
            get { return this.Original.HasAnyValidateMethod; }
        }

        public override ReadOnlyCollection<MetaType> DerivedTypes
        {
            get { return this.Original.DerivedTypes; }
        }

        public override ReadOnlyCollection<MetaDataMember> DataMembers
        {
            get { return this.Original.DataMembers; }
        }

        public override ReadOnlyCollection<MetaDataMember> PersistentDataMembers
        {
            get { return this.Original.PersistentDataMembers; }
        }

        public override ReadOnlyCollection<MetaDataMember> IdentityMembers
        {
            get { return this.Original.IdentityMembers; }
        }

        public override ReadOnlyCollection<MetaAssociation> Associations
        {
            get { return this.Original.Associations; }
        }

        public override MethodInfo OnLoadedMethod
        {
            get { return this.Original.OnLoadedMethod; }
        }

        public override MethodInfo OnValidateMethod
        {
            get { return this.Original.OnValidateMethod; }
        }
    }
}
