using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.BuildFileRelatedMessage;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.BuildFileRelatedMessage
{
    [ExcludeFromCodeCoverage]
    public class Warning2 : TestCaseBase
    {
        public override String Input { get; } = "9>C:\\Some project path\\AppPoolDlg.wxs(9,0): warning CNDL1000: The Binary/@Id attribute's value, 'GetUserCredentialsCA', is 20 characters long.  It will be too long if modularized.  The identifier shouldn't be longer than 18 characters long to allow for modularization (appending a guid for merge modules).\r\n";

        public override BuildFileRelatedMessageData ExpectedResult { get; } = new BuildFileRelatedMessageData(
            new ParsedValue<Int32>(9, new Span(0, 1)),
            new ParsedValue<MessageType>(MessageType.Warning, new Span(44, 7)),
            new ParsedValue<String>("CNDL1000", new Span(52, 8)),
            new ParsedValue<String>("The Binary/@Id attribute's value, 'GetUserCredentialsCA', is 20 characters long.  It will be too long if modularized.  The identifier shouldn't be longer than 18 characters long to allow for modularization (appending a guid for merge modules).", new Span(62, 243)),
            new ParsedValue<String>("warning CNDL1000: The Binary/@Id attribute's value, 'GetUserCredentialsCA', is 20 characters long.  It will be too long if modularized.  The identifier shouldn't be longer than 18 characters long to allow for modularization (appending a guid for merge modules).", new Span(44, 261)),
            new ParsedValue<String>("C:\\Some project path\\AppPoolDlg.wxs", new Span(2, 35))
        );
    }
}