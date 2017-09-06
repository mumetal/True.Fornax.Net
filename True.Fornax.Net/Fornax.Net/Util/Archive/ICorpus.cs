using System;

using Language = Fornax.Net.Common.Culture.Language;

namespace Fornax.Net.Util.Archive
{
    interface ICorpus<TType> :  System.Collections.Generic.IEnumerable<TType>
    {
        DictionaryTrie GetDictionaryTrie(Language language);
    }
}
