using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fornax.Net.Util.Archive
{
    internal class DictionaryTrie
    {
        private DictionaryTrie() {

        }

        //Class for loading English dictionary words and generating trie for words 
        //the trie afterwards serialized into fornaxcorpus.resx via (fornax_en_dict.trie) --- run once
        //write a serializer via protobuf, zeroformatter , xml, binary
        //binary is used to zeroformatter is used to serialize this (DictionaryTrie to *file) -----run once
        //memory stream is then used to write data from [fornax_en_dict.trie] to resx file ----- this code is only run once
        //
    }
}
