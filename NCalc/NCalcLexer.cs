// $ANTLR 3.2 Sep 23, 2009 12:02:23 Grammar\\NCalc.g 2013-07-10 15:34:34


using System;
using Antlr.Runtime;
using IList 		= System.Collections.IList;
using ArrayList 	= System.Collections.ArrayList;
using Stack 		= Antlr.Runtime.Collections.StackList;


public class NCalcLexer : Lexer {
    public const int T__29 = 29;
    public const int T__28 = 28;
    public const int T__27 = 27;
    public const int T__26 = 26;
    public const int T__25 = 25;
    public const int T__24 = 24;
    public const int T__23 = 23;
    public const int LETTER = 20;
    public const int FLOAT = 7;
    public const int HEXINT = 5;
    public const int ID = 12;
    public const int EOF = -1;
    public const int HexDigit = 16;
    public const int BININT = 6;
    public const int NAME = 13;
    public const int T__51 = 51;
    public const int T__52 = 52;
    public const int DIGIT = 15;
    public const int T__50 = 50;
    public const int T__42 = 42;
    public const int INTEGER = 4;
    public const int E = 18;
    public const int T__43 = 43;
    public const int T__40 = 40;
    public const int T__41 = 41;
    public const int T__46 = 46;
    public const int T__47 = 47;
    public const int T__44 = 44;
    public const int T__45 = 45;
    public const int T__48 = 48;
    public const int T__49 = 49;
    public const int DATETIME = 9;
    public const int TRUE = 10;
    public const int T__30 = 30;
    public const int T__31 = 31;
    public const int T__32 = 32;
    public const int WS = 22;
    public const int T__33 = 33;
    public const int T__34 = 34;
    public const int T__35 = 35;
    public const int T__36 = 36;
    public const int T__37 = 37;
    public const int T__38 = 38;
    public const int T__39 = 39;
    public const int BINDIGIT = 17;
    public const int UnicodeEscape = 21;
    public const int IDPART = 14;
    public const int FALSE = 11;
    public const int EscapeSequence = 19;
    public const int STRING = 8;

    // delegates
    // delegators

    public NCalcLexer() 
    {
		InitializeCyclicDFAs();
    }
    public NCalcLexer(ICharStream input)
		: this(input, null) {
    }
    public NCalcLexer(ICharStream input, RecognizerSharedState state)
		: base(input, state) {
		InitializeCyclicDFAs(); 

    }
    
    override public string GrammarFileName
    {
    	get { return "Grammar\\NCalc.g";} 
    }

    // $ANTLR start "T__23"
    public void mT__23() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__23;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:7:7: ( '?' )
            // Grammar\\NCalc.g:7:9: '?'
            {
            	Match('?'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__23"

    // $ANTLR start "T__24"
    public void mT__24() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__24;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:8:7: ( ':' )
            // Grammar\\NCalc.g:8:9: ':'
            {
            	Match(':'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__24"

    // $ANTLR start "T__25"
    public void mT__25() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__25;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:9:7: ( '||' )
            // Grammar\\NCalc.g:9:9: '||'
            {
            	Match("||"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__25"

    // $ANTLR start "T__26"
    public void mT__26() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__26;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:10:7: ( 'or' )
            // Grammar\\NCalc.g:10:9: 'or'
            {
            	Match("or"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__26"

    // $ANTLR start "T__27"
    public void mT__27() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__27;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:11:7: ( '&&' )
            // Grammar\\NCalc.g:11:9: '&&'
            {
            	Match("&&"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__27"

    // $ANTLR start "T__28"
    public void mT__28() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__28;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:12:7: ( 'and' )
            // Grammar\\NCalc.g:12:9: 'and'
            {
            	Match("and"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__28"

    // $ANTLR start "T__29"
    public void mT__29() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__29;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:13:7: ( '|' )
            // Grammar\\NCalc.g:13:9: '|'
            {
            	Match('|'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__29"

    // $ANTLR start "T__30"
    public void mT__30() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__30;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:14:7: ( '^' )
            // Grammar\\NCalc.g:14:9: '^'
            {
            	Match('^'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__30"

    // $ANTLR start "T__31"
    public void mT__31() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__31;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:15:7: ( '&' )
            // Grammar\\NCalc.g:15:9: '&'
            {
            	Match('&'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__31"

    // $ANTLR start "T__32"
    public void mT__32() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__32;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:16:7: ( '==' )
            // Grammar\\NCalc.g:16:9: '=='
            {
            	Match("=="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__32"

    // $ANTLR start "T__33"
    public void mT__33() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__33;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:17:7: ( '=' )
            // Grammar\\NCalc.g:17:9: '='
            {
            	Match('='); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__33"

    // $ANTLR start "T__34"
    public void mT__34() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__34;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:18:7: ( '!=' )
            // Grammar\\NCalc.g:18:9: '!='
            {
            	Match("!="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__34"

    // $ANTLR start "T__35"
    public void mT__35() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__35;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:19:7: ( '<>' )
            // Grammar\\NCalc.g:19:9: '<>'
            {
            	Match("<>"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__35"

    // $ANTLR start "T__36"
    public void mT__36() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__36;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:20:7: ( '<' )
            // Grammar\\NCalc.g:20:9: '<'
            {
            	Match('<'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__36"

    // $ANTLR start "T__37"
    public void mT__37() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__37;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:21:7: ( '<=' )
            // Grammar\\NCalc.g:21:9: '<='
            {
            	Match("<="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__37"

    // $ANTLR start "T__38"
    public void mT__38() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__38;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:22:7: ( '>' )
            // Grammar\\NCalc.g:22:9: '>'
            {
            	Match('>'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__38"

    // $ANTLR start "T__39"
    public void mT__39() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__39;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:23:7: ( '>=' )
            // Grammar\\NCalc.g:23:9: '>='
            {
            	Match(">="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__39"

    // $ANTLR start "T__40"
    public void mT__40() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__40;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:24:7: ( '<<' )
            // Grammar\\NCalc.g:24:9: '<<'
            {
            	Match("<<"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__40"

    // $ANTLR start "T__41"
    public void mT__41() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__41;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:25:7: ( '>>' )
            // Grammar\\NCalc.g:25:9: '>>'
            {
            	Match(">>"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__41"

    // $ANTLR start "T__42"
    public void mT__42() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__42;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:26:7: ( '+' )
            // Grammar\\NCalc.g:26:9: '+'
            {
            	Match('+'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__42"

    // $ANTLR start "T__43"
    public void mT__43() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__43;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:27:7: ( '-' )
            // Grammar\\NCalc.g:27:9: '-'
            {
            	Match('-'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__43"

    // $ANTLR start "T__44"
    public void mT__44() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__44;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:28:7: ( '*' )
            // Grammar\\NCalc.g:28:9: '*'
            {
            	Match('*'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__44"

    // $ANTLR start "T__45"
    public void mT__45() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__45;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:29:7: ( '/' )
            // Grammar\\NCalc.g:29:9: '/'
            {
            	Match('/'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__45"

    // $ANTLR start "T__46"
    public void mT__46() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__46;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:30:7: ( '%' )
            // Grammar\\NCalc.g:30:9: '%'
            {
            	Match('%'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__46"

    // $ANTLR start "T__47"
    public void mT__47() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__47;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:31:7: ( '!' )
            // Grammar\\NCalc.g:31:9: '!'
            {
            	Match('!'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__47"

    // $ANTLR start "T__48"
    public void mT__48() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__48;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:32:7: ( 'not' )
            // Grammar\\NCalc.g:32:9: 'not'
            {
            	Match("not"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__48"

    // $ANTLR start "T__49"
    public void mT__49() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__49;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:33:7: ( '~' )
            // Grammar\\NCalc.g:33:9: '~'
            {
            	Match('~'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__49"

    // $ANTLR start "T__50"
    public void mT__50() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__50;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:34:7: ( '(' )
            // Grammar\\NCalc.g:34:9: '('
            {
            	Match('('); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__50"

    // $ANTLR start "T__51"
    public void mT__51() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__51;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:35:7: ( ')' )
            // Grammar\\NCalc.g:35:9: ')'
            {
            	Match(')'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__51"

    // $ANTLR start "T__52"
    public void mT__52() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__52;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:36:7: ( ',' )
            // Grammar\\NCalc.g:36:9: ','
            {
            	Match(','); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__52"

    // $ANTLR start "TRUE"
    public void mTRUE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = TRUE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:250:2: ( 'true' )
            // Grammar\\NCalc.g:250:4: 'true'
            {
            	Match("true"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "TRUE"

    // $ANTLR start "FALSE"
    public void mFALSE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = FALSE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:254:2: ( 'false' )
            // Grammar\\NCalc.g:254:4: 'false'
            {
            	Match("false"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "FALSE"

    // $ANTLR start "ID"
    public void mID() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ID;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:258:2: ( IDPART ( '.' IDPART )* )
            // Grammar\\NCalc.g:258:5: IDPART ( '.' IDPART )*
            {
            	mIDPART(); 
            	// Grammar\\NCalc.g:258:12: ( '.' IDPART )*
            	do 
            	{
            	    int alt1 = 2;
            	    int LA1_0 = input.LA(1);

            	    if ( (LA1_0 == '.') )
            	    {
            	        alt1 = 1;
            	    }


            	    switch (alt1) 
            		{
            			case 1 :
            			    // Grammar\\NCalc.g:258:13: '.' IDPART
            			    {
            			    	Match('.'); 
            			    	mIDPART(); 

            			    }
            			    break;

            			default:
            			    goto loop1;
            	    }
            	} while (true);

            	loop1:
            		;	// Stops C# compiler whining that label 'loop1' has no statements


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ID"

    // $ANTLR start "INTEGER"
    public void mINTEGER() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INTEGER;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:262:2: ( ( DIGIT )+ )
            // Grammar\\NCalc.g:262:4: ( DIGIT )+
            {
            	// Grammar\\NCalc.g:262:4: ( DIGIT )+
            	int cnt2 = 0;
            	do 
            	{
            	    int alt2 = 2;
            	    int LA2_0 = input.LA(1);

            	    if ( ((LA2_0 >= '0' && LA2_0 <= '9')) )
            	    {
            	        alt2 = 1;
            	    }


            	    switch (alt2) 
            		{
            			case 1 :
            			    // Grammar\\NCalc.g:262:4: DIGIT
            			    {
            			    	mDIGIT(); 

            			    }
            			    break;

            			default:
            			    if ( cnt2 >= 1 ) goto loop2;
            		            EarlyExitException eee2 =
            		                new EarlyExitException(2, input);
            		            throw eee2;
            	    }
            	    cnt2++;
            	} while (true);

            	loop2:
            		;	// Stops C# compiler whining that label 'loop2' has no statements


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INTEGER"

    // $ANTLR start "HEXINT"
    public void mHEXINT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = HEXINT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:266:2: ( '0' ( 'x' | 'X' ) ( HexDigit )+ )
            // Grammar\\NCalc.g:266:6: '0' ( 'x' | 'X' ) ( HexDigit )+
            {
            	Match('0'); 
            	if ( input.LA(1) == 'X' || input.LA(1) == 'x' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}

            	// Grammar\\NCalc.g:266:22: ( HexDigit )+
            	int cnt3 = 0;
            	do 
            	{
            	    int alt3 = 2;
            	    int LA3_0 = input.LA(1);

            	    if ( ((LA3_0 >= '0' && LA3_0 <= '9') || (LA3_0 >= 'A' && LA3_0 <= 'F') || (LA3_0 >= 'a' && LA3_0 <= 'f')) )
            	    {
            	        alt3 = 1;
            	    }


            	    switch (alt3) 
            		{
            			case 1 :
            			    // Grammar\\NCalc.g:266:22: HexDigit
            			    {
            			    	mHexDigit(); 

            			    }
            			    break;

            			default:
            			    if ( cnt3 >= 1 ) goto loop3;
            		            EarlyExitException eee3 =
            		                new EarlyExitException(3, input);
            		            throw eee3;
            	    }
            	    cnt3++;
            	} while (true);

            	loop3:
            		;	// Stops C# compiler whining that label 'loop3' has no statements


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "HEXINT"

    // $ANTLR start "BININT"
    public void mBININT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BININT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:270:2: ( '0' ( 'b' | 'B' ) ( BINDIGIT )+ )
            // Grammar\\NCalc.g:270:6: '0' ( 'b' | 'B' ) ( BINDIGIT )+
            {
            	Match('0'); 
            	if ( input.LA(1) == 'B' || input.LA(1) == 'b' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}

            	// Grammar\\NCalc.g:270:22: ( BINDIGIT )+
            	int cnt4 = 0;
            	do 
            	{
            	    int alt4 = 2;
            	    int LA4_0 = input.LA(1);

            	    if ( ((LA4_0 >= '0' && LA4_0 <= '1')) )
            	    {
            	        alt4 = 1;
            	    }


            	    switch (alt4) 
            		{
            			case 1 :
            			    // Grammar\\NCalc.g:270:22: BINDIGIT
            			    {
            			    	mBINDIGIT(); 

            			    }
            			    break;

            			default:
            			    if ( cnt4 >= 1 ) goto loop4;
            		            EarlyExitException eee4 =
            		                new EarlyExitException(4, input);
            		            throw eee4;
            	    }
            	    cnt4++;
            	} while (true);

            	loop4:
            		;	// Stops C# compiler whining that label 'loop4' has no statements


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BININT"

    // $ANTLR start "FLOAT"
    public void mFLOAT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = FLOAT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:274:2: ( ( DIGIT )* '.' ( DIGIT )+ ( E )? | ( DIGIT )+ E )
            int alt9 = 2;
            alt9 = dfa9.Predict(input);
            switch (alt9) 
            {
                case 1 :
                    // Grammar\\NCalc.g:274:4: ( DIGIT )* '.' ( DIGIT )+ ( E )?
                    {
                    	// Grammar\\NCalc.g:274:4: ( DIGIT )*
                    	do 
                    	{
                    	    int alt5 = 2;
                    	    int LA5_0 = input.LA(1);

                    	    if ( ((LA5_0 >= '0' && LA5_0 <= '9')) )
                    	    {
                    	        alt5 = 1;
                    	    }


                    	    switch (alt5) 
                    		{
                    			case 1 :
                    			    // Grammar\\NCalc.g:274:4: DIGIT
                    			    {
                    			    	mDIGIT(); 

                    			    }
                    			    break;

                    			default:
                    			    goto loop5;
                    	    }
                    	} while (true);

                    	loop5:
                    		;	// Stops C# compiler whining that label 'loop5' has no statements

                    	Match('.'); 
                    	// Grammar\\NCalc.g:274:15: ( DIGIT )+
                    	int cnt6 = 0;
                    	do 
                    	{
                    	    int alt6 = 2;
                    	    int LA6_0 = input.LA(1);

                    	    if ( ((LA6_0 >= '0' && LA6_0 <= '9')) )
                    	    {
                    	        alt6 = 1;
                    	    }


                    	    switch (alt6) 
                    		{
                    			case 1 :
                    			    // Grammar\\NCalc.g:274:15: DIGIT
                    			    {
                    			    	mDIGIT(); 

                    			    }
                    			    break;

                    			default:
                    			    if ( cnt6 >= 1 ) goto loop6;
                    		            EarlyExitException eee6 =
                    		                new EarlyExitException(6, input);
                    		            throw eee6;
                    	    }
                    	    cnt6++;
                    	} while (true);

                    	loop6:
                    		;	// Stops C# compiler whining that label 'loop6' has no statements

                    	// Grammar\\NCalc.g:274:22: ( E )?
                    	int alt7 = 2;
                    	int LA7_0 = input.LA(1);

                    	if ( (LA7_0 == 'E' || LA7_0 == 'e') )
                    	{
                    	    alt7 = 1;
                    	}
                    	switch (alt7) 
                    	{
                    	    case 1 :
                    	        // Grammar\\NCalc.g:274:22: E
                    	        {
                    	        	mE(); 

                    	        }
                    	        break;

                    	}


                    }
                    break;
                case 2 :
                    // Grammar\\NCalc.g:275:4: ( DIGIT )+ E
                    {
                    	// Grammar\\NCalc.g:275:4: ( DIGIT )+
                    	int cnt8 = 0;
                    	do 
                    	{
                    	    int alt8 = 2;
                    	    int LA8_0 = input.LA(1);

                    	    if ( ((LA8_0 >= '0' && LA8_0 <= '9')) )
                    	    {
                    	        alt8 = 1;
                    	    }


                    	    switch (alt8) 
                    		{
                    			case 1 :
                    			    // Grammar\\NCalc.g:275:4: DIGIT
                    			    {
                    			    	mDIGIT(); 

                    			    }
                    			    break;

                    			default:
                    			    if ( cnt8 >= 1 ) goto loop8;
                    		            EarlyExitException eee8 =
                    		                new EarlyExitException(8, input);
                    		            throw eee8;
                    	    }
                    	    cnt8++;
                    	} while (true);

                    	loop8:
                    		;	// Stops C# compiler whining that label 'loop8' has no statements

                    	mE(); 

                    }
                    break;

            }
            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "FLOAT"

    // $ANTLR start "STRING"
    public void mSTRING() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = STRING;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:279:6: ( '\\'' ( EscapeSequence | ( options {greedy=false; } : ~ ( '\\u0000' .. '\\u001f' | '\\\\' | '\\'' ) ) )* '\\'' | '\"' ( EscapeSequence | ( options {greedy=false; } : ~ ( '\\u0000' .. '\\u001f' | '\\\\' | '\"' ) ) )* '\"' )
            int alt12 = 2;
            int LA12_0 = input.LA(1);

            if ( (LA12_0 == '\'') )
            {
                alt12 = 1;
            }
            else if ( (LA12_0 == '\"') )
            {
                alt12 = 2;
            }
            else 
            {
                NoViableAltException nvae_d12s0 =
                    new NoViableAltException("", 12, 0, input);

                throw nvae_d12s0;
            }
            switch (alt12) 
            {
                case 1 :
                    // Grammar\\NCalc.g:279:10: '\\'' ( EscapeSequence | ( options {greedy=false; } : ~ ( '\\u0000' .. '\\u001f' | '\\\\' | '\\'' ) ) )* '\\''
                    {
                    	Match('\''); 
                    	// Grammar\\NCalc.g:279:15: ( EscapeSequence | ( options {greedy=false; } : ~ ( '\\u0000' .. '\\u001f' | '\\\\' | '\\'' ) ) )*
                    	do 
                    	{
                    	    int alt10 = 3;
                    	    int LA10_0 = input.LA(1);

                    	    if ( (LA10_0 == '\\') )
                    	    {
                    	        alt10 = 1;
                    	    }
                    	    else if ( ((LA10_0 >= ' ' && LA10_0 <= '&') || (LA10_0 >= '(' && LA10_0 <= '[') || (LA10_0 >= ']' && LA10_0 <= '\uFFFF')) )
                    	    {
                    	        alt10 = 2;
                    	    }


                    	    switch (alt10) 
                    		{
                    			case 1 :
                    			    // Grammar\\NCalc.g:279:17: EscapeSequence
                    			    {
                    			    	mEscapeSequence(); 

                    			    }
                    			    break;
                    			case 2 :
                    			    // Grammar\\NCalc.g:279:34: ( options {greedy=false; } : ~ ( '\\u0000' .. '\\u001f' | '\\\\' | '\\'' ) )
                    			    {
                    			    	// Grammar\\NCalc.g:279:34: ( options {greedy=false; } : ~ ( '\\u0000' .. '\\u001f' | '\\\\' | '\\'' ) )
                    			    	// Grammar\\NCalc.g:279:61: ~ ( '\\u0000' .. '\\u001f' | '\\\\' | '\\'' )
                    			    	{
                    			    		if ( (input.LA(1) >= ' ' && input.LA(1) <= '&') || (input.LA(1) >= '(' && input.LA(1) <= '[') || (input.LA(1) >= ']' && input.LA(1) <= '\uFFFF') ) 
                    			    		{
                    			    		    input.Consume();

                    			    		}
                    			    		else 
                    			    		{
                    			    		    MismatchedSetException mse = new MismatchedSetException(null,input);
                    			    		    Recover(mse);
                    			    		    throw mse;}


                    			    	}


                    			    }
                    			    break;

                    			default:
                    			    goto loop10;
                    	    }
                    	} while (true);

                    	loop10:
                    		;	// Stops C# compiler whining that label 'loop10' has no statements

                    	Match('\''); 

                    }
                    break;
                case 2 :
                    // Grammar\\NCalc.g:280:10: '\"' ( EscapeSequence | ( options {greedy=false; } : ~ ( '\\u0000' .. '\\u001f' | '\\\\' | '\"' ) ) )* '\"'
                    {
                    	Match('\"'); 
                    	// Grammar\\NCalc.g:280:14: ( EscapeSequence | ( options {greedy=false; } : ~ ( '\\u0000' .. '\\u001f' | '\\\\' | '\"' ) ) )*
                    	do 
                    	{
                    	    int alt11 = 3;
                    	    int LA11_0 = input.LA(1);

                    	    if ( (LA11_0 == '\\') )
                    	    {
                    	        alt11 = 1;
                    	    }
                    	    else if ( ((LA11_0 >= ' ' && LA11_0 <= '!') || (LA11_0 >= '#' && LA11_0 <= '[') || (LA11_0 >= ']' && LA11_0 <= '\uFFFF')) )
                    	    {
                    	        alt11 = 2;
                    	    }


                    	    switch (alt11) 
                    		{
                    			case 1 :
                    			    // Grammar\\NCalc.g:280:16: EscapeSequence
                    			    {
                    			    	mEscapeSequence(); 

                    			    }
                    			    break;
                    			case 2 :
                    			    // Grammar\\NCalc.g:280:33: ( options {greedy=false; } : ~ ( '\\u0000' .. '\\u001f' | '\\\\' | '\"' ) )
                    			    {
                    			    	// Grammar\\NCalc.g:280:33: ( options {greedy=false; } : ~ ( '\\u0000' .. '\\u001f' | '\\\\' | '\"' ) )
                    			    	// Grammar\\NCalc.g:280:60: ~ ( '\\u0000' .. '\\u001f' | '\\\\' | '\"' )
                    			    	{
                    			    		if ( (input.LA(1) >= ' ' && input.LA(1) <= '!') || (input.LA(1) >= '#' && input.LA(1) <= '[') || (input.LA(1) >= ']' && input.LA(1) <= '\uFFFF') ) 
                    			    		{
                    			    		    input.Consume();

                    			    		}
                    			    		else 
                    			    		{
                    			    		    MismatchedSetException mse = new MismatchedSetException(null,input);
                    			    		    Recover(mse);
                    			    		    throw mse;}


                    			    	}


                    			    }
                    			    break;

                    			default:
                    			    goto loop11;
                    	    }
                    	} while (true);

                    	loop11:
                    		;	// Stops C# compiler whining that label 'loop11' has no statements

                    	Match('\"'); 

                    }
                    break;

            }
            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "STRING"

    // $ANTLR start "DATETIME"
    public void mDATETIME() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DATETIME;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:284:3: ( '#' ( options {greedy=false; } : (~ ( '#' ) )* ) '#' )
            // Grammar\\NCalc.g:284:5: '#' ( options {greedy=false; } : (~ ( '#' ) )* ) '#'
            {
            	Match('#'); 
            	// Grammar\\NCalc.g:284:9: ( options {greedy=false; } : (~ ( '#' ) )* )
            	// Grammar\\NCalc.g:284:36: (~ ( '#' ) )*
            	{
            		// Grammar\\NCalc.g:284:36: (~ ( '#' ) )*
            		do 
            		{
            		    int alt13 = 2;
            		    int LA13_0 = input.LA(1);

            		    if ( ((LA13_0 >= '\u0000' && LA13_0 <= '\"') || (LA13_0 >= '$' && LA13_0 <= '\uFFFF')) )
            		    {
            		        alt13 = 1;
            		    }


            		    switch (alt13) 
            			{
            				case 1 :
            				    // Grammar\\NCalc.g:284:36: ~ ( '#' )
            				    {
            				    	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= '\"') || (input.LA(1) >= '$' && input.LA(1) <= '\uFFFF') ) 
            				    	{
            				    	    input.Consume();

            				    	}
            				    	else 
            				    	{
            				    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            				    	    Recover(mse);
            				    	    throw mse;}


            				    }
            				    break;

            				default:
            				    goto loop13;
            		    }
            		} while (true);

            		loop13:
            			;	// Stops C# compiler whining that label 'loop13' has no statements


            	}

            	Match('#'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DATETIME"

    // $ANTLR start "NAME"
    public void mNAME() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = NAME;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:287:6: ( '[' ( options {greedy=false; } : (~ ( ']' ) )* ) ']' )
            // Grammar\\NCalc.g:287:8: '[' ( options {greedy=false; } : (~ ( ']' ) )* ) ']'
            {
            	Match('['); 
            	// Grammar\\NCalc.g:287:12: ( options {greedy=false; } : (~ ( ']' ) )* )
            	// Grammar\\NCalc.g:287:39: (~ ( ']' ) )*
            	{
            		// Grammar\\NCalc.g:287:39: (~ ( ']' ) )*
            		do 
            		{
            		    int alt14 = 2;
            		    int LA14_0 = input.LA(1);

            		    if ( ((LA14_0 >= '\u0000' && LA14_0 <= '\\') || (LA14_0 >= '^' && LA14_0 <= '\uFFFF')) )
            		    {
            		        alt14 = 1;
            		    }


            		    switch (alt14) 
            			{
            				case 1 :
            				    // Grammar\\NCalc.g:287:39: ~ ( ']' )
            				    {
            				    	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= '\\') || (input.LA(1) >= '^' && input.LA(1) <= '\uFFFF') ) 
            				    	{
            				    	    input.Consume();

            				    	}
            				    	else 
            				    	{
            				    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            				    	    Recover(mse);
            				    	    throw mse;}


            				    }
            				    break;

            				default:
            				    goto loop14;
            		    }
            		} while (true);

            		loop14:
            			;	// Stops C# compiler whining that label 'loop14' has no statements


            	}

            	Match(']'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "NAME"

    // $ANTLR start "E"
    public void mE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = E;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:290:3: ( ( 'E' | 'e' ) ( '+' | '-' )? ( DIGIT )+ )
            // Grammar\\NCalc.g:290:5: ( 'E' | 'e' ) ( '+' | '-' )? ( DIGIT )+
            {
            	if ( input.LA(1) == 'E' || input.LA(1) == 'e' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}

            	// Grammar\\NCalc.g:290:15: ( '+' | '-' )?
            	int alt15 = 2;
            	int LA15_0 = input.LA(1);

            	if ( (LA15_0 == '+' || LA15_0 == '-') )
            	{
            	    alt15 = 1;
            	}
            	switch (alt15) 
            	{
            	    case 1 :
            	        // Grammar\\NCalc.g:
            	        {
            	        	if ( input.LA(1) == '+' || input.LA(1) == '-' ) 
            	        	{
            	        	    input.Consume();

            	        	}
            	        	else 
            	        	{
            	        	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	        	    Recover(mse);
            	        	    throw mse;}


            	        }
            	        break;

            	}

            	// Grammar\\NCalc.g:290:26: ( DIGIT )+
            	int cnt16 = 0;
            	do 
            	{
            	    int alt16 = 2;
            	    int LA16_0 = input.LA(1);

            	    if ( ((LA16_0 >= '0' && LA16_0 <= '9')) )
            	    {
            	        alt16 = 1;
            	    }


            	    switch (alt16) 
            		{
            			case 1 :
            			    // Grammar\\NCalc.g:290:26: DIGIT
            			    {
            			    	mDIGIT(); 

            			    }
            			    break;

            			default:
            			    if ( cnt16 >= 1 ) goto loop16;
            		            EarlyExitException eee16 =
            		                new EarlyExitException(16, input);
            		            throw eee16;
            	    }
            	    cnt16++;
            	} while (true);

            	loop16:
            		;	// Stops C# compiler whining that label 'loop16' has no statements


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "E"

    // $ANTLR start "LETTER"
    public void mLETTER() // throws RecognitionException [2]
    {
    		try
    		{
            // Grammar\\NCalc.g:295:2: ( 'a' .. 'z' | 'A' .. 'Z' | '_' )
            // Grammar\\NCalc.g:
            {
            	if ( (input.LA(1) >= 'A' && input.LA(1) <= 'Z') || input.LA(1) == '_' || (input.LA(1) >= 'a' && input.LA(1) <= 'z') ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "LETTER"

    // $ANTLR start "IDPART"
    public void mIDPART() // throws RecognitionException [2]
    {
    		try
    		{
            // Grammar\\NCalc.g:301:2: ( LETTER ( LETTER | DIGIT )* )
            // Grammar\\NCalc.g:301:4: LETTER ( LETTER | DIGIT )*
            {
            	mLETTER(); 
            	// Grammar\\NCalc.g:301:11: ( LETTER | DIGIT )*
            	do 
            	{
            	    int alt17 = 2;
            	    int LA17_0 = input.LA(1);

            	    if ( ((LA17_0 >= '0' && LA17_0 <= '9') || (LA17_0 >= 'A' && LA17_0 <= 'Z') || LA17_0 == '_' || (LA17_0 >= 'a' && LA17_0 <= 'z')) )
            	    {
            	        alt17 = 1;
            	    }


            	    switch (alt17) 
            		{
            			case 1 :
            			    // Grammar\\NCalc.g:
            			    {
            			    	if ( (input.LA(1) >= '0' && input.LA(1) <= '9') || (input.LA(1) >= 'A' && input.LA(1) <= 'Z') || input.LA(1) == '_' || (input.LA(1) >= 'a' && input.LA(1) <= 'z') ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;

            			default:
            			    goto loop17;
            	    }
            	} while (true);

            	loop17:
            		;	// Stops C# compiler whining that label 'loop17' has no statements


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "IDPART"

    // $ANTLR start "DIGIT"
    public void mDIGIT() // throws RecognitionException [2]
    {
    		try
    		{
            // Grammar\\NCalc.g:305:2: ( '0' .. '9' )
            // Grammar\\NCalc.g:305:4: '0' .. '9'
            {
            	MatchRange('0','9'); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "DIGIT"

    // $ANTLR start "BINDIGIT"
    public void mBINDIGIT() // throws RecognitionException [2]
    {
    		try
    		{
            // Grammar\\NCalc.g:309:2: ( '0' .. '1' )
            // Grammar\\NCalc.g:309:4: '0' .. '1'
            {
            	MatchRange('0','1'); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "BINDIGIT"

    // $ANTLR start "EscapeSequence"
    public void mEscapeSequence() // throws RecognitionException [2]
    {
    		try
    		{
            // Grammar\\NCalc.g:313:2: ( '\\\\' ( 'n' | 'r' | 't' | '\\'' | '\\\\' | '\"' | UnicodeEscape ) )
            // Grammar\\NCalc.g:313:4: '\\\\' ( 'n' | 'r' | 't' | '\\'' | '\\\\' | '\"' | UnicodeEscape )
            {
            	Match('\\'); 
            	// Grammar\\NCalc.g:314:4: ( 'n' | 'r' | 't' | '\\'' | '\\\\' | '\"' | UnicodeEscape )
            	int alt18 = 7;
            	switch ( input.LA(1) ) 
            	{
            	case 'n':
            		{
            	    alt18 = 1;
            	    }
            	    break;
            	case 'r':
            		{
            	    alt18 = 2;
            	    }
            	    break;
            	case 't':
            		{
            	    alt18 = 3;
            	    }
            	    break;
            	case '\'':
            		{
            	    alt18 = 4;
            	    }
            	    break;
            	case '\\':
            		{
            	    alt18 = 5;
            	    }
            	    break;
            	case '\"':
            		{
            	    alt18 = 6;
            	    }
            	    break;
            	case 'u':
            		{
            	    alt18 = 7;
            	    }
            	    break;
            		default:
            		    NoViableAltException nvae_d18s0 =
            		        new NoViableAltException("", 18, 0, input);

            		    throw nvae_d18s0;
            	}

            	switch (alt18) 
            	{
            	    case 1 :
            	        // Grammar\\NCalc.g:315:5: 'n'
            	        {
            	        	Match('n'); 

            	        }
            	        break;
            	    case 2 :
            	        // Grammar\\NCalc.g:316:4: 'r'
            	        {
            	        	Match('r'); 

            	        }
            	        break;
            	    case 3 :
            	        // Grammar\\NCalc.g:317:4: 't'
            	        {
            	        	Match('t'); 

            	        }
            	        break;
            	    case 4 :
            	        // Grammar\\NCalc.g:318:4: '\\''
            	        {
            	        	Match('\''); 

            	        }
            	        break;
            	    case 5 :
            	        // Grammar\\NCalc.g:319:4: '\\\\'
            	        {
            	        	Match('\\'); 

            	        }
            	        break;
            	    case 6 :
            	        // Grammar\\NCalc.g:320:6: '\"'
            	        {
            	        	Match('\"'); 

            	        }
            	        break;
            	    case 7 :
            	        // Grammar\\NCalc.g:321:4: UnicodeEscape
            	        {
            	        	mUnicodeEscape(); 

            	        }
            	        break;

            	}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "EscapeSequence"

    // $ANTLR start "HexDigit"
    public void mHexDigit() // throws RecognitionException [2]
    {
    		try
    		{
            // Grammar\\NCalc.g:326:2: ( ( '0' .. '9' | 'a' .. 'f' | 'A' .. 'F' ) )
            // Grammar\\NCalc.g:326:5: ( '0' .. '9' | 'a' .. 'f' | 'A' .. 'F' )
            {
            	if ( (input.LA(1) >= '0' && input.LA(1) <= '9') || (input.LA(1) >= 'A' && input.LA(1) <= 'F') || (input.LA(1) >= 'a' && input.LA(1) <= 'f') ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "HexDigit"

    // $ANTLR start "UnicodeEscape"
    public void mUnicodeEscape() // throws RecognitionException [2]
    {
    		try
    		{
            // Grammar\\NCalc.g:330:6: ( 'u' HexDigit HexDigit HexDigit HexDigit )
            // Grammar\\NCalc.g:330:12: 'u' HexDigit HexDigit HexDigit HexDigit
            {
            	Match('u'); 
            	mHexDigit(); 
            	mHexDigit(); 
            	mHexDigit(); 
            	mHexDigit(); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "UnicodeEscape"

    // $ANTLR start "WS"
    public void mWS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = WS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Grammar\\NCalc.g:334:4: ( ( ' ' | '\\r' | '\\t' | '\\u000C' | '\\n' ) )
            // Grammar\\NCalc.g:334:7: ( ' ' | '\\r' | '\\t' | '\\u000C' | '\\n' )
            {
            	if ( (input.LA(1) >= '\t' && input.LA(1) <= '\n') || (input.LA(1) >= '\f' && input.LA(1) <= '\r') || input.LA(1) == ' ' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}

            	_channel=HIDDEN;

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "WS"

    override public void mTokens() // throws RecognitionException 
    {
        // Grammar\\NCalc.g:1:8: ( T__23 | T__24 | T__25 | T__26 | T__27 | T__28 | T__29 | T__30 | T__31 | T__32 | T__33 | T__34 | T__35 | T__36 | T__37 | T__38 | T__39 | T__40 | T__41 | T__42 | T__43 | T__44 | T__45 | T__46 | T__47 | T__48 | T__49 | T__50 | T__51 | T__52 | TRUE | FALSE | ID | INTEGER | HEXINT | BININT | FLOAT | STRING | DATETIME | NAME | E | WS )
        int alt19 = 42;
        alt19 = dfa19.Predict(input);
        switch (alt19) 
        {
            case 1 :
                // Grammar\\NCalc.g:1:10: T__23
                {
                	mT__23(); 

                }
                break;
            case 2 :
                // Grammar\\NCalc.g:1:16: T__24
                {
                	mT__24(); 

                }
                break;
            case 3 :
                // Grammar\\NCalc.g:1:22: T__25
                {
                	mT__25(); 

                }
                break;
            case 4 :
                // Grammar\\NCalc.g:1:28: T__26
                {
                	mT__26(); 

                }
                break;
            case 5 :
                // Grammar\\NCalc.g:1:34: T__27
                {
                	mT__27(); 

                }
                break;
            case 6 :
                // Grammar\\NCalc.g:1:40: T__28
                {
                	mT__28(); 

                }
                break;
            case 7 :
                // Grammar\\NCalc.g:1:46: T__29
                {
                	mT__29(); 

                }
                break;
            case 8 :
                // Grammar\\NCalc.g:1:52: T__30
                {
                	mT__30(); 

                }
                break;
            case 9 :
                // Grammar\\NCalc.g:1:58: T__31
                {
                	mT__31(); 

                }
                break;
            case 10 :
                // Grammar\\NCalc.g:1:64: T__32
                {
                	mT__32(); 

                }
                break;
            case 11 :
                // Grammar\\NCalc.g:1:70: T__33
                {
                	mT__33(); 

                }
                break;
            case 12 :
                // Grammar\\NCalc.g:1:76: T__34
                {
                	mT__34(); 

                }
                break;
            case 13 :
                // Grammar\\NCalc.g:1:82: T__35
                {
                	mT__35(); 

                }
                break;
            case 14 :
                // Grammar\\NCalc.g:1:88: T__36
                {
                	mT__36(); 

                }
                break;
            case 15 :
                // Grammar\\NCalc.g:1:94: T__37
                {
                	mT__37(); 

                }
                break;
            case 16 :
                // Grammar\\NCalc.g:1:100: T__38
                {
                	mT__38(); 

                }
                break;
            case 17 :
                // Grammar\\NCalc.g:1:106: T__39
                {
                	mT__39(); 

                }
                break;
            case 18 :
                // Grammar\\NCalc.g:1:112: T__40
                {
                	mT__40(); 

                }
                break;
            case 19 :
                // Grammar\\NCalc.g:1:118: T__41
                {
                	mT__41(); 

                }
                break;
            case 20 :
                // Grammar\\NCalc.g:1:124: T__42
                {
                	mT__42(); 

                }
                break;
            case 21 :
                // Grammar\\NCalc.g:1:130: T__43
                {
                	mT__43(); 

                }
                break;
            case 22 :
                // Grammar\\NCalc.g:1:136: T__44
                {
                	mT__44(); 

                }
                break;
            case 23 :
                // Grammar\\NCalc.g:1:142: T__45
                {
                	mT__45(); 

                }
                break;
            case 24 :
                // Grammar\\NCalc.g:1:148: T__46
                {
                	mT__46(); 

                }
                break;
            case 25 :
                // Grammar\\NCalc.g:1:154: T__47
                {
                	mT__47(); 

                }
                break;
            case 26 :
                // Grammar\\NCalc.g:1:160: T__48
                {
                	mT__48(); 

                }
                break;
            case 27 :
                // Grammar\\NCalc.g:1:166: T__49
                {
                	mT__49(); 

                }
                break;
            case 28 :
                // Grammar\\NCalc.g:1:172: T__50
                {
                	mT__50(); 

                }
                break;
            case 29 :
                // Grammar\\NCalc.g:1:178: T__51
                {
                	mT__51(); 

                }
                break;
            case 30 :
                // Grammar\\NCalc.g:1:184: T__52
                {
                	mT__52(); 

                }
                break;
            case 31 :
                // Grammar\\NCalc.g:1:190: TRUE
                {
                	mTRUE(); 

                }
                break;
            case 32 :
                // Grammar\\NCalc.g:1:195: FALSE
                {
                	mFALSE(); 

                }
                break;
            case 33 :
                // Grammar\\NCalc.g:1:201: ID
                {
                	mID(); 

                }
                break;
            case 34 :
                // Grammar\\NCalc.g:1:204: INTEGER
                {
                	mINTEGER(); 

                }
                break;
            case 35 :
                // Grammar\\NCalc.g:1:212: HEXINT
                {
                	mHEXINT(); 

                }
                break;
            case 36 :
                // Grammar\\NCalc.g:1:219: BININT
                {
                	mBININT(); 

                }
                break;
            case 37 :
                // Grammar\\NCalc.g:1:226: FLOAT
                {
                	mFLOAT(); 

                }
                break;
            case 38 :
                // Grammar\\NCalc.g:1:232: STRING
                {
                	mSTRING(); 

                }
                break;
            case 39 :
                // Grammar\\NCalc.g:1:239: DATETIME
                {
                	mDATETIME(); 

                }
                break;
            case 40 :
                // Grammar\\NCalc.g:1:248: NAME
                {
                	mNAME(); 

                }
                break;
            case 41 :
                // Grammar\\NCalc.g:1:253: E
                {
                	mE(); 

                }
                break;
            case 42 :
                // Grammar\\NCalc.g:1:255: WS
                {
                	mWS(); 

                }
                break;

        }

    }


    protected DFA9 dfa9;
    protected DFA19 dfa19;
	private void InitializeCyclicDFAs()
	{
	    this.dfa9 = new DFA9(this);
	    this.dfa19 = new DFA19(this);
	}

    const string DFA9_eotS =
        "\x04\uffff";
    const string DFA9_eofS =
        "\x04\uffff";
    const string DFA9_minS =
        "\x02\x2e\x02\uffff";
    const string DFA9_maxS =
        "\x01\x39\x01\x65\x02\uffff";
    const string DFA9_acceptS =
        "\x02\uffff\x01\x01\x01\x02";
    const string DFA9_specialS =
        "\x04\uffff}>";
    static readonly string[] DFA9_transitionS = {
            "\x01\x02\x01\uffff\x0a\x01",
            "\x01\x02\x01\uffff\x0a\x01\x0b\uffff\x01\x03\x1f\uffff\x01"+
            "\x03",
            "",
            ""
    };

    static readonly short[] DFA9_eot = DFA.UnpackEncodedString(DFA9_eotS);
    static readonly short[] DFA9_eof = DFA.UnpackEncodedString(DFA9_eofS);
    static readonly char[] DFA9_min = DFA.UnpackEncodedStringToUnsignedChars(DFA9_minS);
    static readonly char[] DFA9_max = DFA.UnpackEncodedStringToUnsignedChars(DFA9_maxS);
    static readonly short[] DFA9_accept = DFA.UnpackEncodedString(DFA9_acceptS);
    static readonly short[] DFA9_special = DFA.UnpackEncodedString(DFA9_specialS);
    static readonly short[][] DFA9_transition = DFA.UnpackEncodedStringArray(DFA9_transitionS);

    protected class DFA9 : DFA
    {
        public DFA9(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 9;
            this.eot = DFA9_eot;
            this.eof = DFA9_eof;
            this.min = DFA9_min;
            this.max = DFA9_max;
            this.accept = DFA9_accept;
            this.special = DFA9_special;
            this.transition = DFA9_transition;

        }

        override public string Description
        {
            get { return "273:1: FLOAT : ( ( DIGIT )* '.' ( DIGIT )+ ( E )? | ( DIGIT )+ E );"; }
        }

    }

    const string DFA19_eotS =
        "\x03\uffff\x01\x22\x01\x1f\x01\x25\x01\x1f\x01\uffff\x01\x28\x01"+
        "\x2a\x01\x2e\x01\x31\x05\uffff\x01\x1f\x04\uffff\x03\x1f\x02\x39"+
        "\x08\uffff\x01\x3a\x02\uffff\x01\x1f\x0b\uffff\x04\x1f\x05\uffff"+
        "\x01\x3f\x01\x40\x02\x1f\x02\uffff\x01\x43\x01\x1f\x01\uffff\x01"+
        "\x45\x01\uffff";
    const string DFA19_eofS =
        "\x46\uffff";
    const string DFA19_minS =
        "\x01\x09\x02\uffff\x01\x7c\x01\x72\x01\x26\x01\x6e\x01\uffff\x02"+
        "\x3d\x01\x3c\x01\x3d\x05\uffff\x01\x6f\x04\uffff\x01\x72\x01\x61"+
        "\x01\x2b\x02\x2e\x08\uffff\x01\x2e\x02\uffff\x01\x64\x0b\uffff\x01"+
        "\x74\x01\x75\x01\x6c\x01\x30\x05\uffff\x02\x2e\x01\x65\x01\x73\x02"+
        "\uffff\x01\x2e\x01\x65\x01\uffff\x01\x2e\x01\uffff";
    const string DFA19_maxS =
        "\x01\x7e\x02\uffff\x01\x7c\x01\x72\x01\x26\x01\x6e\x01\uffff\x02"+
        "\x3d\x02\x3e\x05\uffff\x01\x6f\x04\uffff\x01\x72\x01\x61\x01\x39"+
        "\x01\x78\x01\x65\x08\uffff\x01\x7a\x02\uffff\x01\x64\x0b\uffff\x01"+
        "\x74\x01\x75\x01\x6c\x01\x39\x05\uffff\x02\x7a\x01\x65\x01\x73\x02"+
        "\uffff\x01\x7a\x01\x65\x01\uffff\x01\x7a\x01\uffff";
    const string DFA19_acceptS =
        "\x01\uffff\x01\x01\x01\x02\x04\uffff\x01\x08\x04\uffff\x01\x14"+
        "\x01\x15\x01\x16\x01\x17\x01\x18\x01\uffff\x01\x1b\x01\x1c\x01\x1d"+
        "\x01\x1e\x05\uffff\x01\x25\x01\x26\x01\x27\x01\x28\x01\x21\x01\x2a"+
        "\x01\x03\x01\x07\x01\uffff\x01\x05\x01\x09\x01\uffff\x01\x0a\x01"+
        "\x0b\x01\x0c\x01\x19\x01\x0d\x01\x0f\x01\x12\x01\x0e\x01\x11\x01"+
        "\x13\x01\x10\x04\uffff\x01\x29\x01\x23\x01\x24\x01\x22\x01\x04\x04"+
        "\uffff\x01\x06\x01\x1a\x02\uffff\x01\x1f\x01\uffff\x01\x20";
    const string DFA19_specialS =
        "\x46\uffff}>";
    static readonly string[] DFA19_transitionS = {
            "\x02\x20\x01\uffff\x02\x20\x12\uffff\x01\x20\x01\x09\x01\x1c"+
            "\x01\x1d\x01\uffff\x01\x10\x01\x05\x01\x1c\x01\x13\x01\x14\x01"+
            "\x0e\x01\x0c\x01\x15\x01\x0d\x01\x1b\x01\x0f\x01\x19\x09\x1a"+
            "\x01\x02\x01\uffff\x01\x0a\x01\x08\x01\x0b\x01\x01\x01\uffff"+
            "\x04\x1f\x01\x18\x15\x1f\x01\x1e\x02\uffff\x01\x07\x01\x1f\x01"+
            "\uffff\x01\x06\x03\x1f\x01\x18\x01\x17\x07\x1f\x01\x11\x01\x04"+
            "\x04\x1f\x01\x16\x06\x1f\x01\uffff\x01\x03\x01\uffff\x01\x12",
            "",
            "",
            "\x01\x21",
            "\x01\x23",
            "\x01\x24",
            "\x01\x26",
            "",
            "\x01\x27",
            "\x01\x29",
            "\x01\x2d\x01\x2c\x01\x2b",
            "\x01\x2f\x01\x30",
            "",
            "",
            "",
            "",
            "",
            "\x01\x32",
            "",
            "",
            "",
            "",
            "\x01\x33",
            "\x01\x34",
            "\x01\x36\x01\uffff\x01\x36\x02\uffff\x0a\x35",
            "\x01\x1b\x01\uffff\x0a\x1a\x08\uffff\x01\x38\x02\uffff\x01"+
            "\x1b\x12\uffff\x01\x37\x09\uffff\x01\x38\x02\uffff\x01\x1b\x12"+
            "\uffff\x01\x37",
            "\x01\x1b\x01\uffff\x0a\x1a\x0b\uffff\x01\x1b\x1f\uffff\x01"+
            "\x1b",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x01\x1f\x01\uffff\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01"+
            "\x1f\x01\uffff\x1a\x1f",
            "",
            "",
            "\x01\x3b",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x01\x3c",
            "\x01\x3d",
            "\x01\x3e",
            "\x0a\x35",
            "",
            "",
            "",
            "",
            "",
            "\x01\x1f\x01\uffff\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01"+
            "\x1f\x01\uffff\x1a\x1f",
            "\x01\x1f\x01\uffff\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01"+
            "\x1f\x01\uffff\x1a\x1f",
            "\x01\x41",
            "\x01\x42",
            "",
            "",
            "\x01\x1f\x01\uffff\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01"+
            "\x1f\x01\uffff\x1a\x1f",
            "\x01\x44",
            "",
            "\x01\x1f\x01\uffff\x0a\x1f\x07\uffff\x1a\x1f\x04\uffff\x01"+
            "\x1f\x01\uffff\x1a\x1f",
            ""
    };

    static readonly short[] DFA19_eot = DFA.UnpackEncodedString(DFA19_eotS);
    static readonly short[] DFA19_eof = DFA.UnpackEncodedString(DFA19_eofS);
    static readonly char[] DFA19_min = DFA.UnpackEncodedStringToUnsignedChars(DFA19_minS);
    static readonly char[] DFA19_max = DFA.UnpackEncodedStringToUnsignedChars(DFA19_maxS);
    static readonly short[] DFA19_accept = DFA.UnpackEncodedString(DFA19_acceptS);
    static readonly short[] DFA19_special = DFA.UnpackEncodedString(DFA19_specialS);
    static readonly short[][] DFA19_transition = DFA.UnpackEncodedStringArray(DFA19_transitionS);

    protected class DFA19 : DFA
    {
        public DFA19(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 19;
            this.eot = DFA19_eot;
            this.eof = DFA19_eof;
            this.min = DFA19_min;
            this.max = DFA19_max;
            this.accept = DFA19_accept;
            this.special = DFA19_special;
            this.transition = DFA19_transition;

        }

        override public string Description
        {
            get { return "1:1: Tokens : ( T__23 | T__24 | T__25 | T__26 | T__27 | T__28 | T__29 | T__30 | T__31 | T__32 | T__33 | T__34 | T__35 | T__36 | T__37 | T__38 | T__39 | T__40 | T__41 | T__42 | T__43 | T__44 | T__45 | T__46 | T__47 | T__48 | T__49 | T__50 | T__51 | T__52 | TRUE | FALSE | ID | INTEGER | HEXINT | BININT | FLOAT | STRING | DATETIME | NAME | E | WS );"; }
        }

    }

 
    
}
