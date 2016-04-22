import math


class Document:
    __punctuationMarks = ('\'', '.', ',', '-', '\"', '[', ']', '(', ')')

    __stopWords = set()
    __documents = list()

    def __init__(self, category, text):
        self.category = category
        self.text = text
        self.words = None
        self.rawFrequency = dict()
        self.tf = dict()
        self.idf = dict()
        self.termFrequency = dict()

        Document.__documents.append(self)

    def process_document(self):
        self.__delete_punctuation()
        self.__split_words()
        self.__delete_stop_words()
        self.__calculate_term_frequency()

    def __delete_punctuation(self):
        for punctuationMark in Document.__punctuationMarks:
            self.text = self.text.replace(punctuationMark, '')
        self.text = self.text.replace('\n', ' ')

    def __split_words(self):
        self.words = self.text.split(' ')

    def __delete_stop_words(self):
        self.words = [word for word in self.words if word not in Document.__stopWords]
        self.rawFrequency = {word: 0 for word in set(self.words)}
        self.tf = {word: 0 for word in set(self.words)}
        self.idf = {word: 0 for word in set(self.words)}
        self.termFrequency = {word: 0 for word in set(self.words)}

    def __calculate_term_frequency(self):
        self.__calculate_tf()
        self.__calculate_idf()
        for key in self.termFrequency.keys():
            self.termFrequency[key] = self.tf[key] * self.idf[key]

    def __calculate_tf(self):
        self.__calculate_raw_frequency()
        max_raw_frequency = self.__calculate_max_raw_frequency()
        for key in self.tf.keys():
            self.tf[key] = 0.5 + 0.5 * self.rawFrequency[key] / max_raw_frequency

    def __calculate_raw_frequency(self):
        for word in self.words:
            self.rawFrequency[word] += 1

    def __calculate_max_raw_frequency(self):
        max_raw_frequency = 0
        for value in self.rawFrequency.values():
            if value > max_raw_frequency:
                max_raw_frequency = value
        return max_raw_frequency

    def __calculate_idf(self):
        documents_count = Document.__documents.__len__()
        for word in self.idf.keys():
            count = 0
            for document in Document.__documents:
                if word in document.idf.keys():
                    count += 1
            self.idf[word] = math.log(documents_count / count)

    @staticmethod
    def get_stop_words():
        with open('/home/viktor/PycharmProjects/KNN/stop_words', 'r') as file:
            words = file.read().split(', ')
            for word in words:
                Document.__stopWords.add(word)

    @staticmethod
    def calculate_tanimoto_distance(first_document, second_document):
        union = set(first_document.termFrequency.keys()).union(set(second_document.termFrequency.keys()))

        scalar_product = 0
        first_vector_norm = 0
        second_vector_norm = 0

        for item in union:
            if item not in first_document.termFrequency and item not in second_document.termFrequency:
                first_multiplier = 0
                second_multiplier = 0
            elif item not in first_document.termFrequency and item in second_document.termFrequency:
                first_multiplier = 0
                second_multiplier = second_document.termFrequency[item]
            elif item in first_document.termFrequency and item not in second_document.termFrequency:
                first_multiplier = first_document.termFrequency[item]
                second_multiplier = 0
            elif item in first_document.termFrequency and item in second_document.termFrequency:
                first_multiplier = first_document.termFrequency[item]
                second_multiplier = second_document.termFrequency[item]

            scalar_product += first_multiplier * second_multiplier
            first_vector_norm += first_multiplier * first_multiplier
            second_vector_norm += second_multiplier * second_multiplier

        try:
            result = scalar_product / (first_vector_norm + second_vector_norm - scalar_product)
        except ZeroDivisionError:
            result = 0

        return result
