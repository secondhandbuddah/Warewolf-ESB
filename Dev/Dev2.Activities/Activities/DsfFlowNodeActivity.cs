﻿using System;
using System.Activities;
using System.Activities.Presentation;
using System.Activities.Presentation.Model;
using System.Activities.Presentation.View;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Windows;
using Dev2;
using Dev2.Activities;
using Dev2.Common;
using Dev2.Data.Decisions.Operations;
using Dev2.Data.SystemTemplates.Models;
using Dev2.DataList.Contract;
using Dev2.DataList.Contract.Binary_Objects;
using Dev2.DataList.Contract.Value_Objects;
using Dev2.Diagnostics;
using Dev2.Utilities;
using Microsoft.CSharp.Activities;
using Newtonsoft.Json;

// ReSharper disable CheckNamespace
namespace Unlimited.Applications.BusinessDesignStudio.Activities
// ReSharper restore CheckNamespace
{
    public abstract class DsfFlowNodeActivity<TResult> : DsfActivityAbstract<TResult>, IFlowNodeActivity
    {
        // Changing the ExpressionText property of a VisualBasicValue during runtime has no effect. 
        // The expression text is only evaluated and converted to an expression tree when CacheMetadata() is called.
        readonly CSharpValue<TResult> _expression; // BUG 9304 - 2013.05.08 - TWR - Changed type to CSharpValue
        TResult _theResult;

        #region Ctor

        protected DsfFlowNodeActivity(string displayName)
            : this(displayName, DebugDispatcher.Instance, true)
        {
        }

        // BUG 9304 - 2013.05.08 - TWR - Added this constructor for testing purposes
        protected DsfFlowNodeActivity(string displayName, IDebugDispatcher debugDispatcher, bool isAsync = false)
            : base(displayName, debugDispatcher, isAsync)
        {
            _expression = new CSharpValue<TResult>();
        }

        #endregion

        #region ExpressionText

        public string ExpressionText
        {
            get
            {
                return _expression.ExpressionText;
            }
            set
            {
                _expression.ExpressionText = value;
            }
        }

        #endregion

        #region CacheMetadata

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
            //
            // Must use AddChild (which adds children as 'public') otherwise you will get the following exception:
            //
            // The private implementation of activity Decision has the following validation error:
            // Compiler error(s) encountered processing expression t.Eq(d.Get("FirstName",AmbientDataList),"Trevor").
            // 't' is not declared. It may be inaccessible due to its protection level
            // 'd' is not declared. It may be inaccessible due to its protection level
            //
            metadata.AddChild(_expression);
        }

        #endregion

        #region OnExecute

        protected override void OnExecute(NativeActivityContext context)
        {
            IDSFDataObject dataObject = context.GetExtension<IDSFDataObject>();
            if (dataObject != null && dataObject.IsDebugMode())
            {
                DispatchDebugState(context, StateType.Before);
            }
            context.ScheduleActivity(_expression, OnCompleted, OnFaulted);
        }

        #endregion

        #region OnCompleted

        void OnCompleted(NativeActivityContext context, ActivityInstance completedInstance, TResult result)
        {
            IDSFDataObject dataObject = context.GetExtension<IDSFDataObject>();
            Result.Set(context, result);
            _theResult = result;

            if (dataObject != null && (dataObject.IsDebug || ServerLogger.ShouldLog(dataObject.ResourceID)) || dataObject.RemoteInvoke)
            {
                DispatchDebugState(context, StateType.After);
            }

            OnExecutedCompleted(context, false, false);


        }

        #endregion

        #region OnFaulted

        void OnFaulted(NativeActivityFaultContext faultContext, Exception propagatedException, ActivityInstance propagatedFrom)
        {
            IDSFDataObject dataObject = faultContext.GetExtension<IDSFDataObject>();
            if (dataObject != null && dataObject.IsDebugMode())
            {
                DispatchDebugState(faultContext, StateType.After);
            }
            OnExecutedCompleted(faultContext, true, false);
        }

        #endregion   

        #region Get Debug Inputs/Outputs

        // Travis.Frisinger - 28.01.2013 : Amended for Debug
        public override List<DebugItem> GetDebugInputs(IBinaryDataList dataList)
        {
            var result = new List<DebugItem>();
            IDataListCompiler c = DataListFactory.CreateDataListCompiler();

            string val = Dev2DecisionStack.ExtractModelFromWorkflowPersistedData(ExpressionText);

            try
            {
                Dev2DecisionStack dds = c.ConvertFromJsonToModel<Dev2DecisionStack>(val);
                DebugItem itemToAdd = new DebugItem();
                string userModel = dds.GenerateUserFriendlyModel(dataList.UID, dds.Mode);

                foreach(Dev2Decision dev2Decision in dds.TheStack)
                {
                    if(dev2Decision.Col1 != null && DataListUtil.IsEvaluated(dev2Decision.Col1))
                    {
                        userModel = userModel.Replace(dev2Decision.Col1, EvaluateExpressiomToStringValue(dev2Decision.Col1, dds.Mode, dataList, dev2Decision.EvaluationFn));
                        if(DataListUtil.GetRecordsetIndexType(dev2Decision.Col1) == enRecordsetIndexType.Star)
                        {
                            itemToAdd.AddRange(CreateDebugItemsFromString(dev2Decision.Col1, EvaluateExpressiomToStringValue(dev2Decision.Col1.Replace(DataListUtil.ExtractIndexRegionFromRecordset(dev2Decision.Col1), "0"), dds.Mode, dataList, dev2Decision.EvaluationFn), dataList.UID, 0, enDev2ArgumentType.Input));
                        }
                        else
                        {
                            itemToAdd.AddRange(CreateDebugItemsFromString(dev2Decision.Col1, EvaluateExpressiomToStringValue(dev2Decision.Col1, dds.Mode, dataList, dev2Decision.EvaluationFn), dataList.UID, 0, enDev2ArgumentType.Input));
                    }
                    }
                    if(dev2Decision.Col2 != null && DataListUtil.IsEvaluated(dev2Decision.Col2))
                    {
                        userModel = userModel.Replace(dev2Decision.Col2, EvaluateExpressiomToStringValue(dev2Decision.Col2, dds.Mode, dataList, dev2Decision.EvaluationFn));
                        if (DataListUtil.GetRecordsetIndexType(dev2Decision.Col2) == enRecordsetIndexType.Star)
                        {
                            itemToAdd.AddRange(CreateDebugItemsFromString(dev2Decision.Col2, EvaluateExpressiomToStringValue(dev2Decision.Col2.Replace(DataListUtil.ExtractIndexRegionFromRecordset(dev2Decision.Col2), "0"), dds.Mode, dataList, dev2Decision.EvaluationFn), dataList.UID, 0, enDev2ArgumentType.Input));
                        }
                        else
                        {
                            itemToAdd.AddRange(CreateDebugItemsFromString(dev2Decision.Col2, EvaluateExpressiomToStringValue(dev2Decision.Col2, dds.Mode, dataList, dev2Decision.EvaluationFn), dataList.UID, 0, enDev2ArgumentType.Input));
                    }
                    }
                    if(dev2Decision.Col3 != null && DataListUtil.IsEvaluated(dev2Decision.Col3))
                    {
                        userModel = userModel.Replace(dev2Decision.Col3, EvaluateExpressiomToStringValue(dev2Decision.Col3, dds.Mode, dataList, dev2Decision.EvaluationFn));
                        if (DataListUtil.GetRecordsetIndexType(dev2Decision.Col3) == enRecordsetIndexType.Star)
                        {
                            itemToAdd.AddRange(CreateDebugItemsFromString(dev2Decision.Col3, EvaluateExpressiomToStringValue(dev2Decision.Col3.Replace(DataListUtil.ExtractIndexRegionFromRecordset(dev2Decision.Col3), "0"), dds.Mode, dataList, dev2Decision.EvaluationFn), dataList.UID, 0, enDev2ArgumentType.Input));
                        }
                        else
                        {
                            itemToAdd.AddRange(CreateDebugItemsFromString(dev2Decision.Col3, EvaluateExpressiomToStringValue(dev2Decision.Col3, dds.Mode, dataList, dev2Decision.EvaluationFn), dataList.UID, 0, enDev2ArgumentType.Input));
                    }
                }
                }
                result.Add(itemToAdd);

                itemToAdd = new DebugItem();
                itemToAdd.Add(new DebugItemResult { Type = DebugItemResultType.Label, Value = "Statement" });
                itemToAdd.Add(new DebugItemResult { Type = DebugItemResultType.Label, Value = GlobalConstants.EqualsExpression });
                itemToAdd.Add(new DebugItemResult { Type = DebugItemResultType.Value, Value = userModel });
                result.Add(itemToAdd);

            }
            catch(JsonSerializationException)
            {
                Dev2Switch ds = new Dev2Switch() { SwitchVariable = val };

                string userModel = ds.GenerateUserFriendlyModel(dataList.UID, Dev2DecisionMode.AND);

                DebugItem itemToAdd = new DebugItem();
                itemToAdd.Add(new DebugItemResult { Type = DebugItemResultType.Label, Value = "Switch" });
                itemToAdd.Add(new DebugItemResult { Type = DebugItemResultType.Label, Value = GlobalConstants.EqualsExpression });
                itemToAdd.Add(new DebugItemResult { Type = DebugItemResultType.Value, Value = userModel });
                result.Add(itemToAdd);

            }
            catch(Exception e)
            {
                DebugItem itemToAdd = new DebugItem();
                itemToAdd.Add(new DebugItemResult { Type = DebugItemResultType.Label, Value = "Error" });
                itemToAdd.Add(new DebugItemResult { Type = DebugItemResultType.Label, Value = GlobalConstants.EqualsExpression });
                itemToAdd.Add(new DebugItemResult { Type = DebugItemResultType.Value, Value = e.Message });
                result.Add(itemToAdd);
            }

            return result;
        }


        // Travis.Frisinger - 28.01.2013 : Amended for Debug
        public override List<DebugItem> GetDebugOutputs(IBinaryDataList dataList)
        {
            var result = new List<DebugItem>();
            string resultString = _theResult.ToString();
            DebugItem itemToAdd = new DebugItem();
            IDataListCompiler c = DataListFactory.CreateDataListCompiler();
            string val = Dev2DecisionStack.ExtractModelFromWorkflowPersistedData(ExpressionText);

            try
            {
                Dev2DecisionStack dds = c.ConvertFromJsonToModel<Dev2DecisionStack>(val);

                if(_theResult.ToString() == "True")
                {
                    resultString = dds.TrueArmText;
                }
                else if(_theResult.ToString() == "False")
                {
                    resultString = dds.FalseArmText;
                }
                itemToAdd.Add(new DebugItemResult { Type = DebugItemResultType.Value, Value = resultString });
                result.Add(itemToAdd);
            }
            catch(Exception)
            {
                //2013.02.11: Ashley lewis - Bug 8725: Task 8730 - This means it is a swith, not a decision

                itemToAdd.Add(new DebugItemResult { Type = DebugItemResultType.Value, Value = resultString });
                result.Add(itemToAdd);
            }

            return result;

        }

        #endregion

        #region Private Debug Methods

        private string EvaluateExpressiomToStringValue(string Expression, Dev2DecisionMode mode, IBinaryDataList dataList, enDecisionType type)
        {
            string result = string.Empty;
            IDataListCompiler c = DataListFactory.CreateDataListCompiler();

            ErrorResultTO errors = new ErrorResultTO();
            var dlEntry = c.Evaluate(dataList.UID, enActionType.User, Expression, true, out errors);
            if (dlEntry.IsRecordset)
            {
                if (DataListUtil.GetRecordsetIndexType(Expression) == enRecordsetIndexType.Numeric)
                {
                    int index;
                    if (int.TryParse(DataListUtil.ExtractIndexRegionFromRecordset(Expression), out index))
                    {
                        string error;
                        IList<IBinaryDataListItem> listOfCols = dlEntry.FetchRecordAt(index, out error);
                        if(listOfCols != null)
                        {
                            foreach(IBinaryDataListItem binaryDataListItem in listOfCols)
                        {
                            result = binaryDataListItem.TheValue;
                        }
                    }
                }
                }
                else
                {
                    if (DataListUtil.GetRecordsetIndexType(Expression) == enRecordsetIndexType.Star)
                    {
                        IDev2IteratorCollection colItr = Dev2ValueObjectFactory.CreateIteratorCollection();
                        IBinaryDataListEntry Entry = c.Evaluate(dataList.UID, enActionType.User, Expression, false, out errors);
                        IDev2DataListEvaluateIterator col1Iterator = Dev2ValueObjectFactory.CreateEvaluateIterator(Entry);
                        colItr.AddIterator(col1Iterator);

                        bool firstTime = true;
                        while (colItr.HasMoreData())
                        {
                            if (firstTime)
                            {
                                result = colItr.FetchNextRow(col1Iterator).TheValue;
                                firstTime = false;
                            }
                            else
                            {
                                result += " " + mode + " " + colItr.FetchNextRow(col1Iterator).TheValue;
                            }
                        }
                    }
                    else
                    {
                    result = string.Empty;
                }
            }
            }
            else
            {
                IBinaryDataListItem scalarItem = dlEntry.FetchScalar();
                result = scalarItem.TheValue;
            }


            return result;
        }

        #endregion

        #region GetForEachInputs/Outputs

        public override IList<DsfForEachItem> GetForEachInputs()
        {
            return GetForEachItems(ExpressionText);
        }

        public override IList<DsfForEachItem> GetForEachOutputs()
        {
            return GetForEachItems( _theResult.ToString());
        }

        #endregion

        // BUG 9304 - 2013.05.08 - TWR - Added for testing purposes
        public CodeActivity<TResult> GetTheExpression()
        {
            return _expression;
        }

        // BUG 9304 - 2013.05.08 - TWR - Added for testing purposes
        public string GetTheResult()
        {
            return _theResult.ToString();
        }

    }
}
