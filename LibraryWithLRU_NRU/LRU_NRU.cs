using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWithLRU_NRU {
    public class LRU_NRU : IAlgorithm {
        private List<string> listOfSteps { get; set; }
        int lenOfstr;
        private const char SPACE = ' ';

        public List<string> GetSteps() {
            return listOfSteps;
        }


        private string ResultToString(string str, List<BitAndData> res) {
            for (int j = 0; j < res.Count; j++) {
                str += res[j].value.ToString();
                if (res[j].bit) {
                    str += "'";
                } else {
                    str += SPACE;
                }
                str += SPACE;
            }
            return str;
        }
        private string AddSpaces(string str) {
            while (str.Length != lenOfstr) {
                str += SPACE;
            }
            return str;
        }

        private int ZeroPosition(List<BitAndData> res) {
            for (int i = 0; i < res.Count; i++) {
                if (res[i].bit == false)
                    return i;
            }
            return 0;
        }

        private List<BitAndData> FIFOChange(List<BitAndData> blocks, BitAndData num) {
            List<BitAndData> res = new List<BitAndData>();
            for (int i = 0; i < blocks.Count; i++) {
                if (ZeroPosition(blocks) != i)
                    res.Add(blocks[i]);
            }
            res.Add(num);
            return res;
        }

        private List<BitAndData> LRUChange(List<BitAndData> blocks, BitAndData num) {
            List<BitAndData> res = new List<BitAndData>();
            for (int i = 0; i < blocks.Count; i++) {
                if (blocks[i].value != num.value) {
                    res.Add(blocks[i]);
                }
            }
            res.Add(num);
            return res;
        }


        static bool IsСontains(List<BitAndData> res, BitAndData bitAndMessage) {
            foreach (BitAndData m in res) {
                if (m.value == bitAndMessage.value)
                    return true;
            }
            return false;

        }

        public int GetInterrupts(List<int> input, List<bool> modifiedBit, int buffer, int numOfFilled) {
            List<BitAndData> bitAndMessages = new List<BitAndData>();
            for (int i = 0; i < input.Count; i++) {
                bitAndMessages.Add(new BitAndData(input[i], modifiedBit[i]));
            }
            this.listOfSteps = new List<string>();
            this.lenOfstr = buffer * 3 + 4;
            int filledNow = 0;
            int interrupts = 0;
            List<BitAndData> res = new List<BitAndData>();
            foreach (BitAndData bitAndMessage in bitAndMessages) {
                string step = "";
                step += bitAndMessage.value.ToString();
                if (bitAndMessage.bit) {
                    step += "'";
                } else {
                    step += SPACE;
                }
                step += SPACE;
                if (res.Count != buffer) {
                    res.Add(bitAndMessage);
                    step = ResultToString(step, res);

                    if (filledNow != numOfFilled) {
                        step = AddSpaces(step);
                        filledNow++;
                    } else {
                        interrupts++;
                        while (step.Length != lenOfstr - 1) {
                            step += SPACE;
                        }
                        step += "*";
                    }
                } else {
                    if (IsСontains(res, bitAndMessage)) {
                        res = LRUChange(res, bitAndMessage);
                        step = ResultToString(step, res);
                        step = AddSpaces(step);
                    } else {
                        res = FIFOChange(res, bitAndMessage);
                        interrupts++;
                        step = ResultToString(step, res);
                        while (step.Length != lenOfstr - 1) {
                            step += SPACE;
                        }
                        step += "*";
                    }

                }
                listOfSteps.Add(step);
            }
            return interrupts;
        }
        //private List<string> listOfLists { get; set; }
        //int lenOfstr;
        //private const char SPACE = ' ';

        ////public LRU(int buffer) {

        ////}

        //public List<string> GetSteps() {
        //    return listOfLists;
        //}

        //private static List<int> LRUChange(List<int> block, int num) {
        //    List<int> res = new List<int>();
        //    for (int i = 0; i < block.Count; i++) {
        //        if (block[i] != num) {
        //            res.Add(block[i]);
        //        }
        //    }
        //    res.Add(num);
        //    return res;
        //}

        //private static List<int> FIFOChange(List<int> block, int num) {
        //    for (int i = 1; i < block.Count; i++) {
        //        block[i - 1] = block[i];
        //    }
        //    block[block.Count - 1] = num;
        //    return block;
        //}

        //private List<int> FillFirst(List<int> input, int numOfFilled) {
        //    List<int> res = new List<int>();
        //    string list = "";

        //    for (int i = 0; i < numOfFilled; i++) {
        //        if (res.Contains(input[i])) {
        //            res = LRUChange(res, input[i]);
        //        } else {
        //            res.Add(input[i]);
        //            list += input[i].ToString();
        //            list += SPACE;
        //        }
        //        list = ResultToString(list, res);
        //        list = AddSpaces(list);
        //        this.listOfLists.Add(list);
        //        list = "";

        //    }
        //    return res;
        //}

        //private string AddSpaces(string str) {
        //    while (str.Length != lenOfstr) {
        //        str += SPACE;
        //    }
        //    return str;
        //}

        //private string ResultToString(string str, List<int> res) {
        //    for (int j = 0; j < res.Count; j++) {
        //        str += res[j].ToString();
        //        str += SPACE;
        //    }
        //    return str;
        //}

        //public int GetInterrupts(List<int> input, int buffer, int numOfFilled) {
        //    this.listOfLists = new List<string>();
        //    this.lenOfstr = buffer * 2 + 3;

        //    List<int> res = new List<int>();
        //    string list = "";
        //    int interrupts = 0;
        //    bool isInter = false;
        //    if (numOfFilled != 0) {
        //        res = FillFirst(input, numOfFilled);
        //    }
        //    if (numOfFilled == buffer) {
        //        return interrupts;
        //    }
        //    int count = numOfFilled;
        //    do {
        //        if (res.Contains(input[count])) {
        //            res = LRUChange(res, input[count]);
        //        } else {
        //            res.Add(input[count]);
        //            interrupts++;
        //            isInter = true;
        //        }
        //        list += input[count].ToString();
        //        list += SPACE;
        //        list = ResultToString(list, res);
        //        if (isInter) {
        //            while (list.Length != lenOfstr - 1) {
        //                list += SPACE;
        //            }
        //            list += "*";
        //        } else {
        //            list = AddSpaces(list);
        //        }
        //        this.listOfLists.Add(list);
        //        list = "";
        //        count++;
        //        isInter = false;
        //        if (count == input.Count) {
        //            return interrupts;
        //        }
        //    } while (res.Count != buffer);

        //    for (int i = count; i < input.Count; i++) {
        //        if (res.Contains(input[i])) {
        //            res = LRUChange(res, input[i]);
        //        } else {
        //            res = FIFOChange(res, input[i]);
        //            interrupts++;
        //            isInter = true;
        //        }
        //        list += input[count].ToString();
        //        list += SPACE;
        //        list = ResultToString(list, res);
        //        if (isInter) {
        //            while (list.Length != lenOfstr - 1) {
        //                list += SPACE;
        //            }
        //            list += "*";
        //        } else {
        //            list = AddSpaces(list);
        //        }
        //        this.listOfLists.Add(list);
        //        list = "";
        //        count++;
        //        isInter = false;
        //    }

        //    return interrupts;
        //}
    }
}
