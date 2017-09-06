using System;
using System.Collections;
using System.Collections.Generic;

using Language = Fornax.Net.Common.Culture.Language;

namespace Fornax.Net.Util.Archive
{
    internal class FornaxCorpus<TType> : ICorpus<TType> 
    {
        private static DictionaryTrie dictionaryTrie;

        private static SortedSet<string> sortedStops = new SortedSet<string>();

        private FornaxCorpus() {

        }

        public DictionaryTrie GetDictionaryTrie(Language language) {
            throw new NotImplementedException();
        }

        public IEnumerator<TType> GetEnumerator() {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }
    }
}
